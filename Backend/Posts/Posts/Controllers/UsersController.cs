using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.DAL.Repositories;
using Posts.Models;


namespace Posts.Controllers;
[Route("api/users")]
public class UsersController : Controller
{
   private readonly IRepository<User> _users;

   public UsersController(IRepository<User> users)
   {
       _users = users;
   }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
        => Ok(await _users.AddAsync(user));
}

