using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JKM.UTILITY.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly TokenManager _tokenManager;

        public JwtService(IOptions<TokenManager> tokenManager)
        {
            _tokenManager = tokenManager.Value;
        }
                
        public string CreateToken(int idUser, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] secret = Encoding.ASCII.GetBytes(_tokenManager.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("IdUsuario", idUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

        public List<Claim> DecodeToken(string Token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadToken(Token) as JwtSecurityToken;
            return (token.Claims as List<Claim>);
        }

        public bool IsValid(string token)
        {
            try
            {
                byte[] secret = Encoding.ASCII.GetBytes(_tokenManager.Secret);

                new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                }, out SecurityToken validatedToken);
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }
    }
}
