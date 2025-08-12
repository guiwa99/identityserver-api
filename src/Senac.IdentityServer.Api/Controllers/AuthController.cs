using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senac.IdentityServer.Api.Data;
using Senac.IdentityServer.Api.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Senac.IdentityServer.Api.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthController : ControllerBase
  {
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest user)
    {
      string usuario = UsuariosContext.GetUsuariosValidos().FirstOrDefault(x => x.Equals(user.Username));
      if (usuario == null)
      {
        return Unauthorized();
      }

      if (user.Password == "senac123")
      {
        var token = GenerateJwtToken(user.Username);
        return Ok(new { token });
      } else
      {
        return Unauthorized();
      }
    }

    private string GenerateJwtToken(string username)
    {
      var claims = new[]
      {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("V3ry$3cr3tK3y!Sh0uldB3Long&Random"));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: "https://localhost:7245/",
          audience: "https://localhost:7245/",
          claims: claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
