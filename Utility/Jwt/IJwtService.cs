using System.Collections.Generic;
using System.Security.Claims;

namespace JKM.UTILITY.Jwt
{
    public interface IJwtService
    {
        string CreateToken(int idUser, string username);
        List<Claim> DecodeToken(string token);
        bool IsValid(string token);
    }
}
