using Homework_19.Helpers;
using Homework_19.Models;
using Homework_19.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Homework_19.Controllers;

[ApiController]
[Route("api/")]
public class LoginController : Controller
{
    private IUserService _userService;
    private readonly AppSettings _appSettings;
    
    public LoginController(IUserService userService, IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    [EndpointDescription("Login to the system")]
    public IActionResult Login([FromBody] LoginUser loginUser)
    {
        var user = _userService.Login(loginUser);
        if (user is null) return BadRequest(new {message = "Invalid username or password"});
        
        var token = GenerateToken.GetToken(user, _appSettings.Secret);
        
        return Ok(
            new
            {
                token = token
            }
        );
    }

}