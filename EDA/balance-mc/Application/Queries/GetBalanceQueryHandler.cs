using EDA_FC3.Application.Contracts;
using EDA_FC3.Domain.Interfaces;
using MassTransit;

namespace EDA_FC3.Application.Queries;

public class GetBalanceQueryHandler : IConsumer<GetBalancesQuery>
{
    private readonly IBalancesRepository _balancesRepository;

    public GetBalanceQueryHandler(IBalancesRepository balancesRepository)
    {
        _balancesRepository = balancesRepository;
    }

    public async Task Consume(ConsumeContext<GetBalancesQuery> context)
    {
        List<GetBalancesQueryResult> result = null;
        try
        {
            result = await _balancesRepository.ListAsync();
        }
        catch (Exception ex)
        {
            result = new List<GetBalancesQueryResult>()
            {
                new GetBalancesQueryResult()
                {
                    AccountIdFrom = ex.Message
                }
            };
        }

        if (result is null)
            result = new List<GetBalancesQueryResult>();

        await context.RespondAsync<GetBalancesQueryResult[]>(result.ToArray());
    }
}
