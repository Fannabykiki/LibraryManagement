using BookStore.API.DTOs.User;
using BookStore.API.Services.UserService;
using BookStore.Service.Services.Loggerservice;
using Common.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BookStore.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IUsersService _usersService;
        public AuthenticationController(IUsersService usersService, ILoggerManager logger)
        {
            _usersService = usersService;
            _logger = logger;
        }
        [HttpPost("token")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _usersService.LoginUser(request.UserName, request.Password);
            if (user == null)
            {
                return BadRequest();
            }
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim("UserID",user.UserId.ToString()),
                new Claim("UserName", user.UserName.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Key));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expired = DateTime.UtcNow.AddMinutes(JwtConstant.ExpiredTime);

            var token = new JwtSecurityToken(JwtConstant.Issuer,
                JwtConstant.Audience, claims,
                expires: expired, signingCredentials: signIn);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);
        }
    }
}
