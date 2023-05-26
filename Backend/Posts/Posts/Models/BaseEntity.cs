using System;
namespace Posts.Models;

public class BaseEntity
{
    public Guid Gid { get; private set; }
    // Property

    public BaseEntity()
    {
        Gid = Guid.NewGuid();
    }
}

