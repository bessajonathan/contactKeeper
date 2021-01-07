using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactKeeperApi.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<ApplicationSettings> options;

        public TokenService(IOptions<ApplicationSettings> options)
        {
            this.options = options;
        }

        public TokenViewModel GenerateToken(Domain.Entities.User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);

            DateTime createdAt = DateTime.Now;
            DateTime expireDate = createdAt.AddHours(2);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("id",user.Id.ToString()),
                        new Claim("userName",user.UserName),
                    }),
                NotBefore = createdAt,
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return new TokenViewModel
            {
                Token = handler.WriteToken(token),
                CreatedAt = createdAt,
                Expires = expireDate
            };
        }
    }
}
