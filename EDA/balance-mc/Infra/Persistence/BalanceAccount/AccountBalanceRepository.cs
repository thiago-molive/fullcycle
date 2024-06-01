using Dapper;
using EDA_FC3.Domain;
using EDA_FC3.Domain.Interfaces;
using System.Data;

namespace EDA_FC3.Infra.Persistence.BalanceAccount;

public sealed class AccountBalanceRepository : IAccountBalanceRepository
{
    public async Task Save(AccountBalance entity, IDbConnection conn, IDbTransaction trann)
    {
        var param = new DynamicParameters();
        param.Add("@pr_account_id", entity.AccountId, direction: ParameterDirection.Input);
        param.Add("@pr_balance", entity.Balance, direction: ParameterDirection.Input);

        if (conn.State != ConnectionState.Open)
            throw new Exception("Connection statate is not open.");

        try
        {
            var result = await conn.ExecuteAsync(AccountBalanceRepositoryConsts.SQL_SAVE, param, trann);
            if (result == 0)
                throw new Exception("Account balance not inserted");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task Update(AccountBalance entity, IDbConnection conn, IDbTransaction trann)
    {
        var param = new DynamicParameters();
        param.Add("@pr_account_id", entity.AccountId, direction: ParameterDirection.Input);
        param.Add("@pr_balance", entity.Balance, direction: ParameterDirection.Input);
        param.Add("@pr_version", entity.Version, direction: ParameterDirection.Input);

        if (conn.State != ConnectionState.Open)
            throw new Exception("Connection statate is not open.");

        try
        {
            var result = await conn.ExecuteAsync(AccountBalanceRepositoryConsts.SQL_UPDATE, param, trann);

            if (result == 0)
                throw new Exception("Account balance not updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<AccountBalance?> GetByAccountIdAsync(string accountId, IDbConnection conn, IDbTransaction trann)
    {
        var param = new DynamicParameters();
        param.Add("@pr_account_id", accountId, direction: ParameterDirection.Input);

        if (conn.State != ConnectionState.Open)
            throw new Exception("Connection statate is not open.");

        try
        {
            var result = await conn.QueryFirstOrDefaultAsync<AccountBalanceDto>(AccountBalanceRepositoryConsts.SQL_FIND_BY_ACCOUNT_ID, param, trann);

            if (result is null)
                return default;

            return AccountBalanceDto.MapToEntity(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return default;
    }
}