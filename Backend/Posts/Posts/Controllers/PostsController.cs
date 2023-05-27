using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Posts.Controllers.Requests;
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
    public async Task<IActionResult> CreatePost([FromBody] PostRequest request)
    // PostRequest - какие поля я с клиента шлю
    {
        var post = new Post(request.Title, request.Body, request.UserGid);
        return Ok(await _posts.AddAsync(post));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePost([FromBody] Guid gid)
    {
        var post = await _posts.GetByGidAsync(gid);

        if (post is null) return NoContent();

        return Ok(await _posts.DeleteAsync(post));
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost([FromBody] ChangePostRequest request)
    {
        var post = await _posts.GetByGidAsync(request.Gid);

        if (post is null) return NoContent();

        post.Title = request.Title;
        post.Body = request.Body;
        post.UserGid = request.UserGid;

        return Ok(await _posts.UpdateAsync(post));
    }
}
