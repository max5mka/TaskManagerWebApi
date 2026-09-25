using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasOne(p => p.Creator)
                   .WithMany(u => u.CreatedProjects)
                   .HasForeignKey(p => p.CreatorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
