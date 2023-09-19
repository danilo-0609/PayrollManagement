using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayrollManagement.Users.Application.Abstractions;
using PayrollManagement.Users.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PayrollManagement.Users.Infraestructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        public readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string Generate(User user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                new Claim(ClaimTypes.Role, user.Role.Value)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims, 
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            string tokenValue = new JwtSecurityTokenHandler()
                            .WriteToken(token);

            return tokenValue;
        }
    }
}
