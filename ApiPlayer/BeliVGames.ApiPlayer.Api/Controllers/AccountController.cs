using BeliVGames.ApiPlayer.Application.Contracts.Infrastructure;
using BeliVGames.ApiPlayer.Application.Contracts.Persistence;
using BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.CreateJwtBearerToken;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeliVGames.ApiPlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJwtManagerRepository _jWtManager;
    private readonly IJwtBearerTokenRepository _jwtBearerTokenRepository;
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public AccountController(IJwtManagerRepository jWtManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,  RoleManager<IdentityRole> roleManager, IMediator mediator, IJwtBearerTokenRepository jwtBearerTokenRepository)
    {
        _jWtManager = jWtManager;
        _userManager = userManager;
        _signInManager = signInManager;
     
        _roleManager = roleManager;
        _mediator = mediator;
        _jwtBearerTokenRepository = jwtBearerTokenRepository;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> LoginAction([FromBody]LoginModel user)
    {
        if (!ModelState.IsValid)
            return NoContent();
        
        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
        
        if (!result.Succeeded)
            return Unauthorized();
        
        var token = _jWtManager.Authenticate(user);
        ;  
        var tokenInfo = new CreateJwtBearerTokenCommand
        {
            RefreshToken = token.RefreshToken,
            UserName = user.Email
        };
        await _mediator.Send(tokenInfo);
        
        return Ok(token);
    }
 
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAction([FromBody]UserRegisterModel userModel)
    {
        if(!ModelState.IsValid)
        {
            return NoContent();
        }
        
        var user = new IdentityUser { UserName = userModel.Email, Email = userModel.Email };//_mapper.Map<IdentityUser>(userModel);
        var result = await _userManager.CreateAsync(user, userModel.Password);
        if(!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return NoContent();
        }
        await _userManager.AddToRoleAsync(user, "Admin");

        return Ok();
    } 
    
    [HttpPost]
    [Route("creationRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreationRoleAction([FromBody]CreationRoleModel role)
    {
        if (!ModelState.IsValid)
            return NoContent();

        var identityRole = new IdentityRole
        {
            Name = role.NameRole
        };

        var result = await _roleManager.CreateAsync(identityRole);
        if (result.Succeeded)
            return Ok();
        return NoContent();

    }
    
    //[HttpPost(Name = "logout")]
    [HttpPost]
    [Route("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LogoutAction() { 
        await _signInManager.SignOutAsync();
        //Eliminar el token de la base de datos
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("refreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody]Tokens token)
    {
        var principal = _jWtManager.GetPrincipalFromExpiredToken(token.Token);
        var username = principal.Identity?.Name;
        
        if (username == null)
            return Unauthorized("Invalid token!"); 
        
        var savedRefreshToken =_jwtBearerTokenRepository.GetSavedRefreshTokens(username, token.RefreshToken);
            
        if (savedRefreshToken?.RefreshToken == token.RefreshToken)
        {
            return Unauthorized("Invalid attempt!");
        }

        var newJwtToken = _jWtManager.GenerateRefreshToken(username);
        
        _jwtBearerTokenRepository.DeleteUserRefreshTokens(username, token.RefreshToken);
        
        var tokenInfo = new CreateJwtBearerTokenCommand
       {
           RefreshToken = newJwtToken,
           UserName = username
       };
       await _mediator.Send(tokenInfo);

        return Ok(newJwtToken);
    }

}