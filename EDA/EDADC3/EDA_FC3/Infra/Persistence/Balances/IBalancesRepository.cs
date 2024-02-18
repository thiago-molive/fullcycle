using EDA_FC3.Application.Contracts;

namespace EDA_FC3.Infra.Persistence.Balances;

public interface IBalancesRepository
{
    Task Save(Domain.Balances client);

    Task<List<GetBalancesQueryResult>> ListAsync();
}