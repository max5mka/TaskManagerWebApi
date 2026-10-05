using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;

namespace Api.IntegrationTests
{
    [Collection(nameof(IntegrationTestCollection))]
    public class IntegrationTestBase(ApplicationFactory factory) : IAsyncLifetime
    {
        protected readonly HttpClient Client = factory.CreateClient();
        protected readonly ApplicationFactory Factory = factory;

        public async Task InitializeAsync()
        {
            Client.DefaultRequestHeaders.Authorization = null;
            await Factory.ResetDatabaseAsync();
        }

        public async Task DisposeAsync()
        {
            await Factory.ResetDatabaseAsync();
        }

        public async Task<UserEntity> AuthorizeClientAsync(string login)
        {
            var newUser = new UserEntity { FirstName = login, Login = login };
            newUser.HashedPassword = new PasswordHasher<UserEntity>().HashPassword(newUser, login);

            await Factory.SeedEntity(newUser);

            var response = await Client.PostAsJsonAsync("api/login", new UserAuthorizeRequest { Login = login, Password = login });
            var token = await response.Content.ReadAsStringAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return newUser;
        }
    }
}
