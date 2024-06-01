using EDA_FC3.Abstractions;

namespace EDA_FC3.Domain;

public sealed class AccountBalance : EntityBase<long>
{
    public string AccountId { get; private set; }

    public decimal Balance { get; private set; }

    public int Version { get; set; }

    private AccountBalance() { }

    public static AccountBalance Create(string accountId, decimal balance)
    {
        return new()
        {
            AccountId = accountId,
            Balance = balance,
            Version = 0
        };
    }

    public static AccountBalance Restore(long id, string accountId, decimal balance, int version)
    {
        return new()
        {
            Id = id,
            AccountId = accountId,
            Balance = balance,
            Version = version
        };
    }

    public void ChanceBalance(decimal balance) =>
        Balance = balance;
}
