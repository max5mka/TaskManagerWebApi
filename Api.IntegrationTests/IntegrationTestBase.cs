namespace Api.IntegrationTests
{
    [Collection(nameof(IntegrationTestCollection))]
    public class IntegrationTestBase(ApplicationFactory factory) : IAsyncLifetime
    {
        protected readonly HttpClient Client = factory.CreateClient();
        protected readonly ApplicationFactory Factory = factory;

        public async Task DisposeAsync()
        {
            await Factory.ResetDatabaseAsync();
        }

        public async Task InitializeAsync()
        {
            await Factory.ResetDatabaseAsync();
        }
    }
}
