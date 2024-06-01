using System.Data;
using EDA_FC3.Application.Contracts;

namespace EDA_FC3.Domain.Interfaces;

public interface IBalancesRepository
{
    Task Save(Balances client, IDbConnection conn, IDbTransaction trann = null);

    Task<List<GetBalancesQueryResult>> ListAsync();

    Task<GetBalanceByIdQueryResult> GetBalanceByAccountIdAsync(Guid id);
}