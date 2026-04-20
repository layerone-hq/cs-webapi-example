using Microsoft.EntityFrameworkCore;
using WebApiDemo.Domain.Entities;

namespace WebApiDemo.Infrastructure.Persistence;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ToDoItem>().HasKey(t => t.Id);
    }
}
