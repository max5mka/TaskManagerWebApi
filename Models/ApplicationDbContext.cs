using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .HasOne(w => w.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(w => w.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
