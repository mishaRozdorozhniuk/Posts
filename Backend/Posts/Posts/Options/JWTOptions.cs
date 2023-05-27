using System;
namespace Posts.Options;

public class JWTOptions
{
    public string SecretKey { get; set; }
    public int ExpiryMinutes { get; set; }
}

