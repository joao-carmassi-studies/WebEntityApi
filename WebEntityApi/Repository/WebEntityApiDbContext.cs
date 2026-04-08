using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebEntityApi.Models;

namespace WebEntityApi.Repository;

public class WebEntityApiDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebEntityApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
