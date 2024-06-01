using EDA_FC3.Abstractions;

namespace EDA_FC3.Domain;

public sealed class Balances : EntityBase<string>
{
    public string AccountIdFrom { get; private set; }
    public string AccountIdTo { get; private set; }
    public decimal BalanceAccoutIdFrom { get; private set; }
    public decimal BalanceAccoutIdTo { get; private set; }

    private Balances() { }

    public static Balances Create(string accountIdFrom
        , string accountIdTo
        , decimal balanceAccountIdFrom
        , decimal balanceAccountIdTo) =>
        new Balances()
        {
            Id = Guid.NewGuid().ToString(),
            AccountIdFrom = accountIdFrom,
            AccountIdTo = accountIdTo,
            BalanceAccoutIdFrom = balanceAccountIdFrom,
            BalanceAccoutIdTo = balanceAccountIdTo
        };

    public static Balances Restore(string id
        , string accountIdFrom
        , string accountIdTo
        , decimal balanceAccountIdFrom
        , decimal balanceAccountIdTo) =>
        new Balances()
        {
            Id = id,
            AccountIdFrom = accountIdFrom,
            AccountIdTo = accountIdTo,
            BalanceAccoutIdFrom = balanceAccountIdFrom,
            BalanceAccoutIdTo = balanceAccountIdTo
        };
}
