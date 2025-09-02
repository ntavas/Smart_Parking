using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Dtos;
using SmartParking.Application.Services.Interfaces;

namespace SmartParking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            var userDto = await _userService.RegisterAsync(request);
            return Ok(new { Message = "User registered successfully", User = userDto });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Message.Split('\n') });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = await _userService.LoginAsync(request);
            if (token == null)
                return Unauthorized(new { Message = "Invalid email or password." });

            return Ok(new { Token = token });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Message.Split('\n') });
        }
    }
}