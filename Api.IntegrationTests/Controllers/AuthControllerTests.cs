using Api.IntegrationTests.TestData;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace Api.IntegrationTests.Controllers
{
    public class AuthControllerTests(ApplicationFactory factory) : IntegrationTestBase(factory)
    {
        [Fact]
        public async Task Register_ReturnsOk()
        {
            // Arrange
            var request = UsersData.GetSimpleRegisterRequest();

            // Act
            var response = await Client.PostAsJsonAsync("/api/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Register_LoginAlreadyExists()
        {
            // Arrange
            var request = UsersData.GetSimpleRegisterRequest();

            // Act
            await Client.PostAsJsonAsync("/api/register", request);
            var response = await Client.PostAsJsonAsync("/api/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task Login_ReturnsOkAndJwtToken()
        {
            // Arrange
            var registerReq = UsersData.GetSimpleRegisterRequest();
            var authorizeReq = new UserAuthorizeRequest
            {
                Login = "login",
                Password = "password"
            };

            await Client.PostAsJsonAsync("/api/register", registerReq);


            // Act
            var response = await Client.PostAsJsonAsync("/api/login", authorizeReq);


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var token = await response.Content.ReadAsStringAsync();
            Assert.NotNull(token);
            Assert.NotEmpty(token);

            var parts = token.Split('.');
            Assert.Equal(3, parts.Length);
        }

        [Fact]
        public async Task Login_InvalidLogin()
        {
            // Arrange
            var registerReq = UsersData.GetSimpleRegisterRequest();

            var authorizeReq = new UserAuthorizeRequest { Login = "wrongLogin", Password = "password" };

            // Act
            await Client.PostAsJsonAsync("/api/register", registerReq);
            var response = await Client.PostAsJsonAsync("/api/login", authorizeReq);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_InvalidPassword()
        {
            // Arrange
            var registerReq = UsersData.GetSimpleRegisterRequest();
            var authorizeReq = new UserAuthorizeRequest { Login = "login", Password = "wrondPassword" };

            // Act
            await Client.PostAsJsonAsync("/api/register", registerReq);
            var response = await Client.PostAsJsonAsync("/api/login", authorizeReq);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
