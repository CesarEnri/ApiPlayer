using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeliVGames.ApiPlayer.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}