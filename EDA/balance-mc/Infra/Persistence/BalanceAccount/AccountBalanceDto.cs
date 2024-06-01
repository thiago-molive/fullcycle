using EDA_FC3.Domain;

namespace EDA_FC3.Infra.Persistence.BalanceAccount;

internal sealed class AccountBalanceDto
{
    public long Id { get; set; }
    public string AccountId { get; set; }
    public int Balance { get; set; }
    public int Version { get; set; }

    public static AccountBalance MapToEntity(AccountBalanceDto dto) =>
        AccountBalance.Restore(dto.Id, dto.AccountId, dto.Balance, dto.Version);
}