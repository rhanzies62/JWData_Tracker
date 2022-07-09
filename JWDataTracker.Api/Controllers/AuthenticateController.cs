using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWDataTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AuthenticateController : ControllerBase
    {
        private readonly ICongregationUserService _congregationUserService;
        public  readonly IConfiguration _configuration;
        public AuthenticateController(ICongregationUserService congregationUserService, IConfiguration configuration)
        {
            _congregationUserService = congregationUserService;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public Response Post(CongregationUserDto model)
        {
            var response = _congregationUserService.GetUserByUsernameAndPassword(model);
            if (response.IsSuccess)
            {
                var claims = new[] {
                    new Claim("userid",response.Data.CongregationUserId.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMonths(12),
                    signingCredentials: signIn);

                response.Message = new JwtSecurityTokenHandler().WriteToken(token);
            }
            return response;
        }
    }
}
