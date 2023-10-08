using IdentityService.Interfaces;
using IdentityService.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SandboxAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (!_authenticationService.AuthenticateUserDetails(dto)) return Unauthorized();

            const string secretKey = "Shared secret key";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Return the JWT to the client
            return Ok(new { token });
        }
    }
}
