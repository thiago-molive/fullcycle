using EDA_FC3.Domain;
using EDA_FC3.Domain.Interfaces;
using MassTransit;
using System.Data;

namespace EDA_FC3.Infra.Integration;

public static class KafkaConsume
{
    public class KafkaMessageConsumer : IConsumer<KafkaMessage>
    {
        private readonly IBalancesRepository _balancesRepository;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        private readonly IDbConnection _dbConnection;

        public KafkaMessageConsumer(IBalancesRepository balancesRepository
            , IAccountBalanceRepository accountBalanceRepository
            , IDbConnection dbConnection)
        {
            _balancesRepository = balancesRepository;
            _accountBalanceRepository = accountBalanceRepository;
            _dbConnection = dbConnection;
        }

        public async Task Consume(ConsumeContext<KafkaMessage> context)
        {
            var entity = Balances.Create(context.Message.Payload.Account_id_from
                , context.Message.Payload.Account_id_to
                , context.Message.Payload.Balance_account_id_from
                , context.Message.Payload.Balance_account_id_to);

            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            using var trann = _dbConnection.BeginTransaction();

            try
            {
                await _balancesRepository.Save(entity, _dbConnection, trann);

                await SaveAccountBalance(entity.AccountIdFrom, entity.BalanceAccoutIdFrom, trann);
                await SaveAccountBalance(entity.AccountIdTo, entity.BalanceAccoutIdTo, trann);

                trann.Commit();
            }
            catch (Exception ex)
            {
                trann.Rollback();

                await context.NotifyFaulted(TimeSpan.MinValue, "", ex);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        private async Task SaveAccountBalance(string accountId, decimal balance, IDbTransaction trann)
        {
            var balanceAccount = await _accountBalanceRepository.GetByAccountIdAsync(accountId, _dbConnection, trann) ?? AccountBalance.Create(accountId, balance);

            if (balanceAccount.Id > 0)
            {
                balanceAccount.ChanceBalance(balance);
                await _accountBalanceRepository.Update(balanceAccount, _dbConnection, trann);
                return;
            }

            await _accountBalanceRepository.Save(balanceAccount, _dbConnection, trann);
        }
    }

    public record KafkaMessage
    {
        public string Name { get; init; }
        public Payload Payload { get; set; }
    }

    public record Payload
    {
        public string Account_id_from { get; set; }
        public string Account_id_to { get; set; }
        public decimal Balance_account_id_from { get; set; }
        public decimal Balance_account_id_to { get; set; }
    }
}
