using System;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.DAL;

public class PostContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    // DbSet - это будет табличка

    public PostContext(DbContextOptions<PostContext> options) : base(options) { }
    // Берет данные с JSON и манипулирует ими

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    // Регистриация конфигураций
}
