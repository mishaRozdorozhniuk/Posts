using System;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.DAL.Repositories;

public interface IRepository<T> where T : BaseEntity
    // T - тупо параметр который после :

    // Репозиторий будет взаимодействовать с базой
    // Дженерик переиспользованный (user, post)
{
    Task<IEnumerable<T>> GetListAsync();
    // с помощю Task - реализованный многопоточный ассинхронный код
    // IEnumerable - базовый интерфейс для коллекций листов и таких прикоов 
    Task<T> GetByGidAsync(Guid gid);
    Task<bool> AddAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    // Methods
}

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly PostContext _postContext;
    // слой работы с базой
    // Dependency Injection
    // Наш контекст чтобы взаимодействовать между C# и БД

    internal DbSet<T> _dbSet;
    // получаем DbSet из вне.
    // DbSet чтобы с базой работать через дженерик

    public Repository(PostContext postContext)
    {
        _postContext = postContext;
        this._dbSet = _postContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetListAsync()
        => await _dbSet.AsNoTracking().ToListAsync<T>();
    // AsNoTracking - само чтобы работало
    // ToListAsync - к списку дженерика

    public async Task<T> GetByGidAsync(Guid gid)
        => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Gid == gid);

    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity); // git commit
        await _postContext.SaveChangesAsync(); // git push

        return true;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        await _dbSet
            .Where(x => x.Gid == entity.Gid)
            .ExecuteDeleteAsync(); // git commit

        await _postContext.SaveChangesAsync(); // git push

        return true;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity); // git commit
        await _postContext.SaveChangesAsync(); // git push

        return true;
    }
}
