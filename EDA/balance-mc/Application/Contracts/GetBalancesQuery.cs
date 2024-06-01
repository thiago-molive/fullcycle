namespace EDA_FC3.Application.Contracts;

public record GetBalancesQuery
{
}

public record GetBalancesQueryResult
{
    public string Id { get; set; }
    public string AccountIdFrom { get; set; }
    public string AccountIdTo { get; set; }
    public decimal BalanceAccountIdFrom { get; set; }
    public decimal BalanceAccountIdTo { get; set; }
}
