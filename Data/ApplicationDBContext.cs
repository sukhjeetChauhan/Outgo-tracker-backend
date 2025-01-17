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
    }
  }
}