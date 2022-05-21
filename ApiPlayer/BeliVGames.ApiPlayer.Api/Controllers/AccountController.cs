using BeliVGames.ApiPlayer.Api.Models;
using BeliVGames.ApiPlayer.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeliVGames.ApiPlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    
    private readonly IJwtManagerRepository _jWTManager;

    public AccountController(IJwtManagerRepository jWTManager)
    {
        this._jWTManager = jWTManager;
    }
    
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
    
    
    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public IActionResult Authenticate([FromBody]LoginModel user)
    {
        
        
        var token = _jWTManager.Authenticate(user);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}