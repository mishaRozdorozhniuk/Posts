using System;
namespace Posts.Controllers.Requests;

public class PostRequest
{
    public string Title { get; set; }
    public string Body { get; set; }
    public Guid UserGid { get; set; }
}

public class ChangePostRequest
{
    public Guid Gid { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public Guid UserGid { get; set; }
}
