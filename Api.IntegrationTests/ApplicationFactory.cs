using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Api.IntegrationTests
{
    public class ApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithImage();
        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
