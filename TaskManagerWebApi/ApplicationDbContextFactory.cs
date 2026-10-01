using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskManagerWebApi.Data;

namespace TaskManagerWebApi
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=TaskManager;User Id=sa;Password=Password123!;TrustServerCertificate=True;Encrypt=False"
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
