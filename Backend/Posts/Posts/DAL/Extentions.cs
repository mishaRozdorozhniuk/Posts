using System;
using Microsoft.EntityFrameworkCore;
using Posts.DAL.Repositories;

namespace Posts.DAL;

public static class Extentions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    // Расширяем IServiceCollection, и первый параметр в програме не передаем
    {
        services.AddDbContext<PostContext>(options =>
        {
            options.UseNpgsql(configuration["ConnectionString"]);
        });
        // регестрируем базу

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // Есть пару видов регистриции наших сервисов
        // AddSingleton - создастся 1 раз на все приложение (1 екземпляр)
        // AddScoped - пока не оборветсья HTTP соединения с конкретной сущностью
        // AddTransient - на каждый запрос создается новый екземпляр
        // слева интерфейс а справа его реализация
        services.AddHostedService<DatabaseInitializer<PostContext>>();
        return services;
    }
}

