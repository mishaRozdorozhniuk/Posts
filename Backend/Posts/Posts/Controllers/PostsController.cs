using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.DAL.Repositories;
using Posts.Models;

namespace Posts.Controllers;

[Route("api/posts")]
public class PostsController : Controller
{
    private readonly IRepository<Post> _posts;

    public PostsController(IRepository<Post> posts)
    {
        _posts = posts;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
        => Ok(await _posts.GetListAsync());

    [HttpGet]
    public async Task<IActionResult> GetListByFiltersAsync([FromQuery] Guid userGid)
        => Ok(await _posts.GetListByFiltersAsync(x => x.UserGid == userGid));

    [HttpGet("{gid}")]
    public async Task<IActionResult> GetPostsById(Guid gid)
        => Ok(await _posts.GetByGidAsync(gid));

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
        => Ok(await _posts.AddAsync(post));

    [HttpDelete]
    public async Task<IActionResult> DeletePost([FromBody] Post post)
        => Ok(await _posts.DeleteAsync(post));

    [HttpPut]
    public async Task<IActionResult> UpdatePost([FromBody] Post post)
       => Ok(await _posts.UpdateAsync(post));
}
