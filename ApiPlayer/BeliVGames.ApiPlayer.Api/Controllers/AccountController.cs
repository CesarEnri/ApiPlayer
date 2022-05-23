using AutoMapper;
using BeliVGames.ApiPlayer.Api.Repository;
using BeliVGames.ApiPlayer.Application.Features.JwtBearerToken.Commands.CreateJwtBearerToken;
using BeliVGames.ApiPlayer.Domain.Entities;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeliVGames.ApiPlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    
    
    private readonly IJwtManagerRepository _jWtManager;
  // private readonly IUserServiceRepository _userServiceRepository;
    
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(IJwtManagerRepository jWtManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IMediator mediator)
    {
        _jWtManager = jWtManager;
        //_userServiceRepository = userServiceRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _mediator = mediator;
    }


    [AllowAnonymous]
    //[HttpPost(Name = "login")]
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAction(LoginModel user)
    {
        if (!ModelState.IsValid)
            return NoContent();
        
        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
        
        if (!result.Succeeded)
            return Unauthorized();
        
        var token = _jWtManager.Authenticate(user);
        
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
    public async Task<IActionResult> RegisterAction(UserRegisterModel userModel)
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
    public async Task<IActionResult> CreationRoleAction(CreationRoleModel role)
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
    //[HttpPost(Name = "refreshToken")]
    [HttpPost]
    [Route("refreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RefreshToken(Tokens token)
    {
        var principal = _jWtManager.GetPrincipalFromExpiredToken(token.Token);
        var username = principal.Identity?.Name;

        //retrieve the saved refresh token from database
       // var savedRefreshToken = _userServiceRepository.GetSavedRefreshTokens(username, token.RefreshToken);

        //if (savedRefreshToken?.RefreshToken != token.RefreshToken)
        //{
        //    return Unauthorized("Invalid attempt!");
        //}

        var newJwtToken = _jWtManager.GenerateRefreshToken(username);

        // saving refresh token to the db
        var obj = new UserRefreshTokens
        {
            RefreshToken = newJwtToken,
            UserName = username
        };

      //  _userServiceRepository.DeleteUserRefreshTokens(username, token.RefreshToken);
       // _userServiceRepository.AddUserRefreshTokens(obj);
//_userServiceRepository.SaveCommit();

        return Ok(newJwtToken);
    }

     //[HttpPost(Name = "saveToken")]
     [HttpPost]
     [Route("saveToken")]
     public async Task<ActionResult<Guid>> SaveTokenUser(CreateJwtBearerTokenCommand createJwtBearerTokenCommand)
     {
         var id = await _mediator.Send(createJwtBearerTokenCommand);
         return Ok();
     }

}