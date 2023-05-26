using System;
using Microsoft.EntityFrameworkCore;
// EntityFrameworkCore - промежуточный слой между си шарп и базой данных
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.Models;
namespace Posts.DAL.Configurations;

public class PostsConfigurations : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Gid);
        builder.Property(x => x.Title).HasMaxLength(15);
        builder.Property(x => x.Body).HasMaxLength(25);
    }
}


