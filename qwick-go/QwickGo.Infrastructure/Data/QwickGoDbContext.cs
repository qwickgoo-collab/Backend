using Microsoft.EntityFrameworkCore;
using QwickGo.Core.Entities;

namespace QwickGo.Infrastructure.Data;

public class QwickGoDbContext : DbContext
{
    public QwickGoDbContext(DbContextOptions<QwickGoDbContext> options) : base(options){ }

    public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<User>()
    .Property(u => u.Role)
    .HasConversion<string>();
    }
}