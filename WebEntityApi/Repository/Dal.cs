using Microsoft.EntityFrameworkCore;

namespace WebEntityApi.Repository;

public class Dal<T> where T : class
{
    public WebEntityApiDbContext context { get; set; }

    public Dal(WebEntityApiDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<T> List()
    {
        return context.Set<T>().ToList();
    }

    public void Add(T obj)
    {
        context.Set<T>().Add(obj);
        context.SaveChanges();
    }

    public T? Find(Func<T, bool> condition)
    {
        return context.Set<T>().FirstOrDefault(condition);
    }

    public void Remove(T obj)
    {
        context.Set<T>().Remove(obj);
        context.SaveChanges();
    }

    public void Update(T obj)
    {
        context.Set<T>().Update(obj);
        context.SaveChanges();
    }
}
