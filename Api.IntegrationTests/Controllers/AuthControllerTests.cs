using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using TaskManagerWebApi.Models.Requests;

namespace Api.IntegrationTests.Controllers
{
    public class AuthControllerTests(ApplicationFactory factory) : IntegrationTestBase(factory)
    {
        [Fact]
        public async Task Register_ShouldReturnNoContent()
        {
            // Arrange
            var request = new UserRegisterRequest
            {
                FirstName = "Name",
                Login = "login",
                Password = "password"
            };

            // Act
            var response = await Client.PostAsJsonAsync("/api/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
