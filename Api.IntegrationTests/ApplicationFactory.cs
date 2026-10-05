using Docker.DotNet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using TaskManagerWebApi.Data;
using Testcontainers.MsSql;

namespace Api.IntegrationTests
{
    public class ApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Password123!")
            .Build();

        private SqlConnection _dbConnection = null!;
        private Respawner _respawner = null!;

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            using var scope = Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await db.Database.MigrateAsync();

            _dbConnection = new SqlConnection(_dbContainer.GetConnectionString());
            await _dbConnection.OpenAsync();

            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
                TablesToIgnore = ["__EFMigrationsHistory"]
            });
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            if (_dbConnection != null)
                await _dbConnection.CloseAsync();

            await _dbContainer.StopAsync();
            await base.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((_, cfg) =>
                cfg.AddInMemoryCollection([
                    new KeyValuePair<string, string?>("ConnectionStrings:DefaultConnection", _dbContainer.GetConnectionString())
                ])
            );
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public async Task<TEntity[]> SeedEntity<TEntity>(params TEntity[] entities) where TEntity : class
        {
            using var scope = Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await db.Set<TEntity>().AddRangeAsync(entities);
            await db.SaveChangesAsync();

            return entities;
        }
    }
}
