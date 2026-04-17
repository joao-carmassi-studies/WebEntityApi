using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebEntityApi.Repository;

public class Dal<T> where T : class
{
    public WebEntityApiDbContext context { get; set; }

    public Dal(WebEntityApiDbContext context)
    {
        this.context = context;
    }

    async public Task<IEnumerable<T>> ListAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    async public Task AddAsync(T obj)
    {
        await context.Set<T>().AddAsync(obj);
        await context.SaveChangesAsync();
    }

    async public Task<T?> FindAsync(Expression<Func<T, bool>> condition)
    {
        return await context.Set<T>().FirstOrDefaultAsync(condition);
    }

    async public Task Remove(T obj)
    {
        context.Set<T>().Remove(obj);
        await context.SaveChangesAsync();
    }

    async public Task Update(T obj)
    {
        context.Set<T>().Update(obj);
        await context.SaveChangesAsync();
    }
}
