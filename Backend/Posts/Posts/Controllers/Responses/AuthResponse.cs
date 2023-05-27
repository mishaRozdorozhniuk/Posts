using System;
namespace Posts.Controllers.Responses;

public class AuthResponse
{
    public string JwtToken { get; set; }
    public Guid UserGid { get; set; }

    public AuthResponse(string jwtToken, Guid userGid)
    {
        JwtToken = jwtToken;
        UserGid = userGid;
    }
}

