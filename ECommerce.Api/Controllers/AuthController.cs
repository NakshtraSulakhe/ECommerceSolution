using ECommerce.Application.DTOs;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly IUserRoleRepository _userRoleRepo;

    public AuthController(IAuthService auth, IUserRoleRepository userRoleRepo)
    {
        _auth = auth;
        _userRoleRepo = userRoleRepo;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = await _auth.Register(dto.FullName, dto.Email, dto.Password);
        if (user == null)
            return BadRequest("Email already exists");

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _auth.Login(dto.Email, dto.Password);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _auth.GenerateToken(user);

        return Ok(new { token });
    }
}
