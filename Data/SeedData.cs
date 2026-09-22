using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Data
{
    public static class SeedData
    {
        public static async Task EnsureAdminCreatedAsync(ApplicationDbContext context)
        {
            var adminExists = await context.Users.AnyAsync(u => u.IsAdmin);
            if (adminExists) return;

            var admin = new UserEntity
            {
                FirstName = "Admin",
                Login = "admin",
                IsAdmin = true
            };

            admin.HashedPassword = new PasswordHasher<UserEntity>().HashPassword(admin, "Admin123!");

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
