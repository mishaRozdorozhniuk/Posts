using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posts.Models;
namespace Posts.DAL.Configurations;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Gid);
        // HasKey - показывает кокое свойство с класса мы будем использовать в качестве первичного ключа
        builder.Property(x => x.FirstName).HasMaxLength(15);
        builder.Property(x => x.LastName).HasMaxLength(15);
        builder.HasMany(p => p.Posts).WithOne(u => u.User);
        // Показываем что User может иметь несколько Posts (Регестрируем связи)
    }
}