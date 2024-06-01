namespace EDA_FC3.Application.Contracts;

public sealed record GetBalanceByIdQuery
{
    public Guid Id { get; set; }
}

public record GetBalanceByIdQueryResult
{
    public string AccountId { get; set; }
    public decimal AccountBalance { get; set; }
}


