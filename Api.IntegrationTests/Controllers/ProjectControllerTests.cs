using Api.IntegrationTests.TestData;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace Api.IntegrationTests.Controllers
{
    public class ProjectControllerTests(ApplicationFactory factory) : IntegrationTestBase(factory)
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetAll_ReturnsOkAndProjectsList(bool hasProjects)
        {
            // Arrange
            var user = await AuthorizeClientAsync("login");
            var projects = new List<ProjectEntity>();

            if (hasProjects)
            {
                projects = (await Factory.SeedEntity(ProjectsData.GetProjects(user.Id).ToArray())).ToList();
                foreach (var p in projects)
                {
                    await Factory.SeedEntity(new UserProject { UserId = user.Id, ProjectId = p.Id, ProjectRole = ProjectRoles.Owner });
                }
            }

            // Act
            var response = await Client.GetAsync("/api/projects");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<ProjectShortResponse>>();
            Assert.NotNull(result);
            Assert.Equal(projects.Count, result.Count);

            foreach (var project in projects)
            {
                var projectResponse = result.SingleOrDefault(p => p.Id == project.Id);
                Assert.NotNull(projectResponse);
                Assert.Equal(project.Title, projectResponse.Title);
                Assert.Equal(project.Status, projectResponse.Status);
                Assert.Equal(project.Start, projectResponse.Start);
                Assert.Equal(project.Deadline, projectResponse.Deadline);
                Assert.Equal(project.CreatedAt, projectResponse.CreatedAt);
                Assert.Equal(project.UpdatedAt, projectResponse.UpdatedAt);
            }
        }

        [Theory]
        [ClassData(typeof(ProjectsData))]
        public async Task GetMyProjectById_ReturnsOkAndProject(TestProjects testProject)
        {
            // Arrange
            var user = await AuthorizeClientAsync("login");

            var project = ProjectsData.CreateProject(testProject, user.Id);
            await Factory.SeedEntity(project);

            var userProject = new UserProject { UserId = user.Id, ProjectId = project.Id, ProjectRole = ProjectRoles.Owner };
            await Factory.SeedEntity(userProject);
    
            // Act
            var response = await Client.GetAsync($"/api/projects/{project.Id}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<ProjectLongResponse>();
            Assert.NotNull(result);

            Assert.Equal(project.Id, result.Id);
            Assert.Equal(project.Title, result.Title);
            Assert.Equal(project.Description, result.Description);
            Assert.Equal(project.Status, result.Status);
            Assert.Equal(project.Start, result.Start);
            Assert.Equal(project.Deadline, result.Deadline);
            Assert.Equal(project.CreatedAt, result.CreatedAt);
            Assert.Equal(project.UpdatedAt, result.UpdatedAt);
        }

        [Theory]
        [ClassData(typeof(ProjectsData))]
        public async Task GetNotMyProjectById_ReturnsNotFound(TestProjects testProject)
        {
            // Arrange
            var user1 = await AuthorizeClientAsync("login1");

            var project = ProjectsData.CreateProject(testProject, user1.Id);
            await Factory.SeedEntity(project);

            var userProject = new UserProject { UserId = user1.Id, ProjectId = project.Id, ProjectRole = ProjectRoles.Owner };
            await Factory.SeedEntity(userProject);

            var user2 = await AuthorizeClientAsync("login2");

            // Act
            var response = await Client.GetAsync($"/api/projects/{project.Id}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ProjectsData))]
        public async Task CreateProject_ReturnsCreatedAndProject(TestProjects tp)
        {
            // Arrange
            var user = await AuthorizeClientAsync("login");
            var projectReq = new ProjectCreateRequest
            {
                Title = tp.Title,
                Description = tp.Description,
                Deadline = tp.Deadline, 
                Start = tp.Start
            };

            // Act
            var response = await Client.PostAsJsonAsync("api/projects", projectReq);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<ProjectCreateResponse>();
            Assert.NotNull(result);
            Assert.Equal(projectReq.Title, result.Title);
            Assert.Equal(projectReq.Description, result.Description);
            Assert.Equal(projectReq.Start, result.Start);
            Assert.Equal(projectReq.Deadline, result.DeadLine);
            Assert.Equal(Statuses.New, result.Status);
        }
    }
}
