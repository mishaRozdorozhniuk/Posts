using System;
namespace Posts.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Post> Posts { get; set; }

    public User(string firstName, string lastName)
        : base()
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

