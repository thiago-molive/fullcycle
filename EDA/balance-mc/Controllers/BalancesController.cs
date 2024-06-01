using EDA_FC3.Application.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EDA_FC3.Controllers;

[Route("v1/[controller]")]
[Produces("application/json")]
[ApiController]
[Description("Controller inicial")]
public class BalancesController : ControllerBase
{
    IRequestClient<GetBalancesQuery> _client;
    IRequestClient<GetBalanceByIdQuery> _clientGetById;

    public BalancesController(IRequestClient<GetBalancesQuery> client
        , IRequestClient<GetBalanceByIdQuery> clientGetById)
    {
        _client = client;
        _clientGetById = clientGetById;
    }


    [HttpGet]
    public async Task<IActionResult> GetBalancesAsync()
    {
        Response<GetBalancesQueryResult[]> response = await _client.GetResponse<GetBalancesQueryResult[]>(new { });
        return Ok(response.Message);
    }

    [HttpGet("{account_id}")]
    public async Task<IActionResult> GetBalancesByIdAsync([FromRoute] string account_id)
    {
        if (!Guid.TryParse(account_id, out var guidId))
            return BadRequest("the account_id isn't an valid identifier.");

        var query = new GetBalanceByIdQuery()
        {
            Id = guidId
        };
        Response<GetBalanceByIdQueryResult> response = await _clientGetById.GetResponse<GetBalanceByIdQueryResult>(query);
        return Ok(response.Message);
    }

}
