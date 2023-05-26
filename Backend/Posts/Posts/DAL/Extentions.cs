using System;
using Microsoft.EntityFrameworkCore;
namespace Posts.DAL;

public static class Extentions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    // Расширяем IServiceCollection
    {
        services.AddDbContext<PostContext>(options =>
        {
            options.UseNpgsql(configuration["ConnectionString"]);
        });
        // регестрируем базу

        return services;
    }
}

