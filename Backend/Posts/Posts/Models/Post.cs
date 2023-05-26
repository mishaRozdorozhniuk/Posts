using System;
namespace Posts.Models;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public Guid UserGid { get; set; }
    public User User { get; set; }

    public Post(string title, string body, Guid userGid)
        : base()
    {
        Title = title;
        Body = body;
        UserGid = userGid;
    }
}


