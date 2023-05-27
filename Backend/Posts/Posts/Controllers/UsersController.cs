using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Posts.Controllers.Requests;
using Posts.Controllers.Responses;
using Posts.DAL.Repositories;
using Posts.Models;
using Posts.Options;

namespace Posts.Controllers;
[Route("api/users")]
public class UsersController : Controller
{
    private readonly IRepository<User> _users;
    private readonly JWTOptions _jwtOptions;

    public UsersController(IRepository<User> users, IOptions<JWTOptions> jwtOptions)
    {
        _users = users;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
    {
        var user = new User(request.FirstName, request.LastName);

        var createdUser = await _users.AddAsync(user); // -> bool

        if(createdUser is not null)
            // если юзер создался то мы генерируем для него токен и возвращяем юзер айди на фронт
            return Ok(new AuthResponse(GenerationJwtToken(user), user.Gid));
        // если не создался то 400 ерор
        return BadRequest();
    }

    private string GenerationJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
               new Claim("id", user.Gid.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
        };

        JwtSecurityToken token = new JwtSecurityToken(
            "http://localhost:5000",
            "http://localhost:8081",
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

