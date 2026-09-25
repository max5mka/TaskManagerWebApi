using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
