using EDA_FC3.Application.Contracts;

namespace EDA_FC3.Infra.Persistence.Balances;

public sealed class BalancesRepositoryConsts
{
    public const string SQL_SAVE = @"INSERT INTO balances (ID, ACCOUNT_ID_FROM, ACCOUNT_ID_TO, BALANCE_ACCOUNT_ID_FROM, BALANCE_ACCOUNT_ID_TO) VALUES (@pr_id, @pr_account_id_from, @pr_account_id_to, @pr_balance_account_id_from, @pr_balance_account_id_to)";

    public const string SQL_LIST = $@"SELECT ID
                                            , ACCOUNT_ID_FROM {nameof(GetBalancesQueryResult.AccountIdFrom)}
                                            , ACCOUNT_ID_TO {nameof(GetBalancesQueryResult.AccountIdTo)}
                                            , BALANCE_ACCOUNT_ID_FROM {nameof(GetBalancesQueryResult.BalanceAccountIdFrom)}
                                            , BALANCE_ACCOUNT_ID_TO {nameof(GetBalancesQueryResult.BalanceAccountIdTo)}
                                            FROM balances";

    public const string SQL_GET_BY_ID = $@"SELECT account_id {nameof(GetBalanceByIdQueryResult.AccountId)}
                                            , balance {nameof(GetBalanceByIdQueryResult.AccountBalance)}
                                            FROM account_balances
                                            WHERE ACCOUNT_ID = @pr_account_id";
}