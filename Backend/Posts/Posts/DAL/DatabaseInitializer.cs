using System;
using Microsoft.EntityFrameworkCore;

namespace Posts.DAL;

internal sealed class DatabaseInitializer<TContext> : IHostedService where TContext : DbContext
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
        var dbContext = (DbContext)scope.ServiceProvider.GetRequiredService<TContext>();
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        // EnsureCreatedAsync - если таблиц ДБСЕТ нет, то он их создаст
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}