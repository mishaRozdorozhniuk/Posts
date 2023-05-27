using System;
using Microsoft.EntityFrameworkCore;

namespace Posts.DAL;

internal sealed class DatabaseInitializer<TContext> : IHostedService where TContext : DbContext
    // модификаторы доступа (public...). internal - виден внутри библиотеки. sealed - невозможность наследования
    // Класс для проверки наличия таблиц при запуске
    // IHostedService - интерфейс который реализовывает интерфейс IHostedService один раз
{
    private readonly IServiceProvider _serviceProvider;
    // IServiceProvider - доставать зависимости
    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        // scope - содержит всю инфу о нашем приложении (интерфейсы, классы)
        var dbContext = (DbContext)scope.ServiceProvider.GetRequiredService<TContext>();
        // dbContext- Ищет все классы которые наследуются от ДБ контекст (PostContext)
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        // EnsureCreatedAsync - если таблиц ДБСЕТ нет, то он их создаст
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}