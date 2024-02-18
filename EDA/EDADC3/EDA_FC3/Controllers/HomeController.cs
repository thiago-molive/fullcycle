using Microsoft.AspNetCore.Mvc;

namespace EDA_FC3.Controllers;

public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index() =>
        Redirect("~/swagger");
}
