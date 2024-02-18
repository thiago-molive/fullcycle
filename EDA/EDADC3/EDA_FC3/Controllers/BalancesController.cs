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

    public BalancesController(IRequestClient<GetBalancesQuery> client)
    {
        _client = client;
    }


    [HttpGet]
    public async Task<IActionResult> GetBalances()
    {
        Response<GetBalancesQueryResult[]> response = await _client.GetResponse<GetBalancesQueryResult[]>(new { });
        return Ok(response.Message);
    }

}
