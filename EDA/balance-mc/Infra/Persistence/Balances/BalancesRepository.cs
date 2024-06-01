using Dapper;
using EDA_FC3.Application.Contracts;
using EDA_FC3.Domain.Interfaces;
using System.Data;

namespace EDA_FC3.Infra.Persistence.Balances;

public sealed class BalancesRepository : IBalancesRepository
{
    private readonly IDbConnection _connection;

    public BalancesRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task Save(Domain.Balances client, IDbConnection conn, IDbTransaction trann = null)
    {
        var param = new DynamicParameters();
        param.Add("@pr_id", client.Id, direction: ParameterDirection.Input);
        param.Add("@pr_account_id_from", client.AccountIdFrom, direction: ParameterDirection.Input);
        param.Add("@pr_account_id_to", client.AccountIdTo, direction: ParameterDirection.Input);
        param.Add("@pr_balance_account_id_from", client.BalanceAccoutIdFrom, direction: ParameterDirection.Input);
        param.Add("@pr_balance_account_id_to", client.BalanceAccoutIdTo, direction: ParameterDirection.Input);

        if (conn.State != ConnectionState.Open)
            throw new Exception("Connection state is not open.");

        try
        {
            var result = await conn.ExecuteAsync(BalancesRepositoryConsts.SQL_SAVE, param);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<List<GetBalancesQueryResult>> ListAsync()
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        try
        {
            var result = await _connection.QueryAsync<GetBalancesQueryResult>(BalancesRepositoryConsts.SQL_LIST);

            return result.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Enumerable.Empty<GetBalancesQueryResult>().ToList();
        }
        finally
        {
            _connection.Close();
        }
    }

    public async Task<GetBalanceByIdQueryResult> GetBalanceByAccountIdAsync(Guid id)
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        try
        {
            var param = new DynamicParameters();
            param.Add("@pr_account_id", id, direction: ParameterDirection.Input);

            var result = await _connection.QueryFirstOrDefaultAsync<GetBalanceByIdQueryResult>(BalancesRepositoryConsts.SQL_GET_BY_ID, param);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new GetBalanceByIdQueryResult();
        }
        finally
        {
            _connection.Close();
        }
    }
}