using AutoMapper;
using BeliVGames.ApiPlayer.Api.Models;
using BeliVGames.ApiPlayer.Api.Repository;
using BeliVGames.ApiPlayer.Domain.Helpers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeliVGames.ApiPlayer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IJwtManagerRepository _jWtManager;
  // private readonly IUserServiceRepository _userServiceRepository;
    
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(IJwtManagerRepository jWtManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _jWtManager = jWtManager;
        //_userServiceRepository = userServiceRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel user)
    {
        if (!ModelState.IsValid)
            return NoContent();
        
        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
        
        if (!result.Succeeded)
            return Unauthorized();
        
        var token = _jWtManager.Authenticate(user);
        
        var obj = new UserRefreshTokens
        {               
            RefreshToken = token.RefreshToken,
            UserName = user.Email
        };
        
        //_userServiceRepository.AddUserRefreshTokens(obj);
        //_userServiceRepository.SaveCommit();
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost]
    //[ValidateAntiForgeryToken]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]UserRegisterModel userModel)
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
    public async Task<IActionResult> CreationRole(CreationRoleModel role)
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
    
    [HttpPost] 
    [Route("logout")]
    public async Task<IActionResult> Logout() { 
        await _signInManager.SignOutAsync();
        //Eliminar el token de la base de datos
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh(Tokens token)
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
    
    
}