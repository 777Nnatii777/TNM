using Microsoft.EntityFrameworkCore;
using Domain.Entites; 
public class MyApplicationDbContext : DbContext
{
    public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } 
}
