using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.DTOs.AppUserDTOs;
using ProjektAPBD.Models;
using ProjektAPBD.SecurityHelpers;
using ProjektAPBD.Services;

namespace ProjektAPBD.Controllers;

[Route("api/authetification")]
[ApiController]
public class UserAuthetificationController : ControllerBase
{

    private readonly IUserService _userService;

    public UserAuthetificationController(IUserService userService)
    {
        _userService = userService; 
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequestDTO requestDto)
    {
        await _userService.RegisterUser(requestDto);

        return Created();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO loginRequestDto)
    {
        
        var result = await _userService.LoginUser(loginRequestDto);
        
        return Ok(result);
    }

    // nie dziala na swaggerze
    // [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequestDTO refreshToken)
    {
        UserOutputDTO result = await _userService.GetRefreshToken(refreshToken);
        
        return Ok(result);
    }
    
}