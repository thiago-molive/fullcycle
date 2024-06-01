using EDA_FC3.Application.Contracts;
using EDA_FC3.Domain.Interfaces;
using MassTransit;

namespace EDA_FC3.Application.Queries;

public class GetBalanceByIdQueryHandler : IConsumer<GetBalanceByIdQuery>
{
    private readonly IBalancesRepository _balancesRepository;

    public GetBalanceByIdQueryHandler(IBalancesRepository balancesRepository)
    {
        _balancesRepository = balancesRepository;
    }

    public async Task Consume(ConsumeContext<GetBalanceByIdQuery> context)
    {
        GetBalanceByIdQueryResult result = null;
        try
        {
            result = await _balancesRepository.GetBalanceByAccountIdAsync(context.Message.Id);
        }
        catch (Exception ex)
        {
        }

        if (result is null)
            result = new GetBalanceByIdQueryResult();

        await context.RespondAsync(result);
    }
}
