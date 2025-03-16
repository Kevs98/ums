using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UMS.Application.Common.Interfaces;

namespace UMS.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly ITotpService _otpService;
        private readonly UserService _userService;


        public AuthenticationService(IConfiguration configuration, ITotpService otpService, UserService userService)
        {
            _configuration = configuration;
            _otpService = otpService;
            _userService = userService;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var dbUser = await _userService.GetByUsername(username, password);
            if (dbUser != null)
            {

                var otpSecret = _otpService.GenerateSecretKey();
                return otpSecret;
            }

            return null;
        }

        public async Task<string> GenerateJwtAsync(string username)
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "user")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:issuer"],
                audience: _configuration["Jwt:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
