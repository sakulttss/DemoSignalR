using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JwtController : ControllerBase
{
    public const string Issuer = "ChatJwt";
    public const string Audience = "ChatJwt";
    private static readonly SecurityKey SigningKey = new RsaSecurityKey(RSA.Create());
    private static readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();
    public static readonly SigningCredentials SigningCreds = new SigningCredentials(SigningKey, SecurityAlgorithms.RsaSha256);

    [HttpGet("login")]
    public IActionResult Login([FromQuery] string username)
    {
        var token = JwtTokenHandler.CreateJwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            signingCredentials: SigningCreds,
            expires: DateTime.UtcNow.AddDays(1),
            subject: new([new Claim(ClaimTypes.NameIdentifier, username)])
        );
        return Ok(JwtTokenHandler.WriteToken(token));
    }
}