using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.DTOs;
using ProjectManager.API.Services;

namespace ProjectManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
  private readonly AuthService _auth;
  private readonly JwtService _jwt;
  public AuthController(AuthService auth, JwtService jwt) { _auth = auth; _jwt = jwt; }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterDto dto) {
    var user = await _auth.Register(dto.Username, dto.Password);
    if (user == null) return BadRequest(new { message = "Username already exists" });
    var token = _jwt.Generate(user);
    return Ok(new { token });
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDto dto) {
    var user = await _auth.Validate(dto.Username, dto.Password);
    if (user == null) return Unauthorized(new { message = "Invalid credentials" });
    var token = _jwt.Generate(user);
    return Ok(new { token });
  }
}
