using BookStore.API.DTOs.User;
using BookStore.API.Services.UserService;
using BookStore.Common.DTOs.User;
using BookStore.Data.Entities;
using BookStore.Extensions;
using BookStore.Service.Services.Loggerservice;
using Common.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
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
        private readonly ClaimsIdentity? _identity;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationController(IUsersService usersService, ILoggerManager logger, IHttpContextAccessor httpContextAccessor)
        {
            _usersService = usersService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            var identity = httpContextAccessor.HttpContext?.User?.Identity;
            if (identity == null)
            {
                _identity = null;
            }
            else
            {
                _identity = identity as ClaimsIdentity;
            }
        }
        [HttpPost("token")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _usersService.LoginUser(request.UserName, request.Password);
            if (user == null)
            {
                return NotFound();
            }
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim("UserId",user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
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

        [HttpGet("current-user")]
        public  IActionResult GetCurrentLoginUser()
        {
            var current = this.GetCurrentLoginUserId();
            return Ok(current);
        }
    }
}
