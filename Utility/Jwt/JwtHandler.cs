using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JKM.UTILITY.Jwt
{
    public static class JwtHandler
    {
        private static byte[] secret = null;

        public static void setSecret(byte[] key)
        {
            if(secret == null)  secret = key;
        }

        public static IEnumerable<Claim> decodeToken(string Token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadToken(Token) as JwtSecurityToken;
            IEnumerable<Claim> a = token.Claims;
            return a;
        }

        public static string createToken()
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Cesar"),
                new Claim(ClaimTypes.Email, "cesarfb")
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

        public static bool validate(string token)
        {
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                }, out SecurityToken validatedToken);
            }
            catch(Exception error)
            {
                return false;
            }
            return true;
        }
    }
}
