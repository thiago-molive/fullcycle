using System.Data;

namespace EDA_FC3.Domain.Interfaces;

public interface IAccountBalanceRepository
{
    Task Save(AccountBalance entity, IDbConnection conn, IDbTransaction trann = null);

    Task<AccountBalance?> GetByAccountIdAsync(string accountId, IDbConnection conn, IDbTransaction trann = null);

    Task Update(AccountBalance entity, IDbConnection conn, IDbTransaction trann = null);
}