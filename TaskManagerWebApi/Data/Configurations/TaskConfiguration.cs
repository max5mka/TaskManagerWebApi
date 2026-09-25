using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasIndex(u => u.Title).IsUnique();

            builder.HasOne(w => w.Project)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(w => w.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.Creator)
                   .WithMany(p => p.CreatedTasks)
                   .HasForeignKey(w => w.CreatorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
