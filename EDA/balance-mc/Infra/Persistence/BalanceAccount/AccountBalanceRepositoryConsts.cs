namespace EDA_FC3.Infra.Persistence.BalanceAccount;

public sealed class AccountBalanceRepositoryConsts
{
    public const string SQL_FIND_BY_ACCOUNT_ID = $@"SELECT id as {nameof(AccountBalanceDto.Id)}
                                                        , account_id as {nameof(AccountBalanceDto.AccountId)}
                                                        , balance as {nameof(AccountBalanceDto.Balance)}
                                                        , version as {nameof(AccountBalanceDto.Version)}
                                                  FROM account_balances
                                                  WHERE account_id = @pr_account_id
                                                  FOR UPDATE SKIP LOCKED";

    public const string SQL_SAVE = @"INSERT INTO account_balances (account_id, balance) VALUES (@pr_account_id, @pr_balance)";

    public const string SQL_UPDATE = @"UPDATE account_balances
                                        SET balance = @pr_balance,
                                            version = version + 1
                                        WHERE account_id = @pr_account_id 
                                        AND version = @pr_version";
}