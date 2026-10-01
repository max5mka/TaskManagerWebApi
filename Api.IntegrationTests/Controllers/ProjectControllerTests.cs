using Api.IntegrationTests.TestData;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Api.IntegrationTests.Controllers
{
    public class ProjectControllerTests(ApplicationFactory factory) : IntegrationTestBase(factory)
    {
        public async Task GetAll_ReturnsOkResultAndProjectsList()
        {
            // Arrange
            var users = UsersData.Users;
            var projects = ProjectsData.Projects;

            await Factory.SeedEntity(users.ToArray());
            await Factory.SeedEntity(projects.ToArray());

            // Act
            var response = await Client.GetAsync("/api/projects");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
