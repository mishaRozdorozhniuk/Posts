using System;
using Microsoft.EntityFrameworkCore;
using Posts.DAL.Repositories;

namespace Posts.DAL;

public static class Extentions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    // Встроеный интерфейс в сишарпе
    // Расширяем IServiceCollection, и первый параметр в програме не передаем
    {
        services.AddDbContext<PostContext>(options =>
        // регестрируем базу
        {
            var connectionString = configuration["ConnectionString"];

            options.UseNpgsql(connectionString);
            // UseNpgsql - будем использовать именно постгрес, и передаем ConnectionString с джейсона чтобы установить соединение
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // Регестрируем сущности, (cущность и реализация)
        // Есть пару видов регистриции наших сервисов
        // AddSingleton - создастся 1 раз на все приложение (1 екземпляр)
        // AddScoped - пока не оборветсья HTTP соединения с конкретной сущностью
        // AddTransient - на каждый запрос создается новый екземпляр
        // слева интерфейс а справа его реализация
        services.AddHostedService<DatabaseInitializer<PostContext>>();
        // резестрируем логику DatabaseInitializer класса
        return services;
    }
}

