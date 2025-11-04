using Microsoft.AspNetCore.Mvc;
using MaaPateshwariUniversity.Application.DTOs;
using MaaPateshwariUniversity.Application.Interfaces;

namespace MaaPateshwariUniversity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth){ _auth = auth; }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var res = await _auth.LoginAsync(req);
        if (!res.Success) return Unauthorized(new { message = "Invalid credentials" });
        return Ok(res);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh(RefreshRequest req)
    {
        var token = await _auth.RefreshAsync(req);
        if (token == null) return Unauthorized();
        return Ok(new { token });
    }
}
