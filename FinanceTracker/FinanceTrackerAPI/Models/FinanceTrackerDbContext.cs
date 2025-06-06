using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Models;

public class FinanceTrackerDbContext : DbContext
{
    public FinanceTrackerDbContext(DbContextOptions<FinanceTrackerDbContext> options)
        : base(options) { }

    public DbSet<Expense> Expenses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>().ToTable("Expenses");
        base.OnModelCreating(modelBuilder);
    }
}
