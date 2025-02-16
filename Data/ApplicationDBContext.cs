using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Models;

namespace Outgo_tracker_Backend.Data
{

  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
    {
    }
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<ProjectUser> ProjectUsers { get; set; } = default!;
    public DbSet<Expense> Expenses { get; set; } = default!;
    public DbSet<Income> Incomes { get; set; } = default!;
    public DbSet<ProjectJoinRequest> ProjectJoinRequests { get; set; } = default!;

    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProjectUser>()
          .HasKey(pu => pu.Id); // Primary key

      modelBuilder.Entity<ProjectUser>()
          .HasIndex(pu => new { pu.ProjectId, pu.UserId })
          .IsUnique(); // Ensure combination of ProjectId and UserId is unique

      modelBuilder.Entity<ProjectUser>()
          .HasOne(pu => pu.Project)
          .WithMany(p => p.ProjectUsers)
          .HasForeignKey(pu => pu.ProjectId);

      modelBuilder.Entity<Expense>().HasKey(e => e.Id); // Primary key

      modelBuilder.Entity<Expense>()
          .HasOne(e => e.Project)
          .WithMany(p => p.Expenses)
          .HasForeignKey(e => e.ProjectId);

      modelBuilder.Entity<Income>().HasKey(i => i.Id); // Primary key

      modelBuilder.Entity<Income>()
          .HasOne(i => i.Project)
          .WithMany(p => p.Incomes)
          .HasForeignKey(i => i.ProjectId);
    }
  }
}