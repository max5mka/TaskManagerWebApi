using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Entities;

namespace Api.IntegrationTests.TestData
{
    public record TestProjects(string Title, string Description, string Status, DateTime Start, DateTime Deadline);

    public class ProjectsData : IEnumerable<object[]>
    {
        public static readonly List<TestProjects> TestProjects =
        [
            new("title1", "Descr1", "New", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)),
            new("title2", "Descr2", "In Progress", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4)),
            new("title3", "Descr3", "In Progress", DateTime.Now.AddDays(1), DateTime.Now.AddDays(6)),
        ];

        public IEnumerator<object[]> GetEnumerator()
        {
            return TestProjects.Select(p => new object[] { p }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static List<ProjectEntity> GetProjects(int userId)
        {
            var result = new List<ProjectEntity>();
            TestProjects.ForEach(p => result.Add(CreateProject(p, userId)));

            return result;
        }

        public static ProjectEntity CreateProject(TestProjects testProject, int userId)
        {
            return new ProjectEntity
            {
                Title = testProject.Title,
                Description = testProject.Description,
                Status = testProject.Status,
                Start = testProject.Start,
                Deadline = testProject.Deadline,
                CreatorId = userId,
            };
        }
    }

    /*public class ProjectsData : IEnumerable<object[]>
    {
        public static readonly List<ProjectEntity> Projects =
        [
            new() {
                Title = "title1", 
                Description = "Descr1", 
                Status = "New", 
                Start = DateTime.Now.AddDays(1), 
                Deadline = DateTime.Now.AddDays(2),
                CreatorId = UsersData.Users[0].Id,
                Creator = UsersData.Users[0],
            },
            new() {
                Title = "title2",
                Description = "Descr2",
                Status = "In Progress",
                Start = DateTime.Now.AddDays(1),
                Deadline = DateTime.Now.AddDays(4),
                CreatorId = UsersData.Users[0].Id,
                Creator = UsersData.Users[0],
            },
            new() {
                Title = "title3",
                Description = "Descr3",
                Status = "In Progress",
                Start = DateTime.Now.AddDays(1),
                Deadline = DateTime.Now.AddDays(6),
                CreatorId = UsersData.Users[1].Id,
                Creator = UsersData.Users[1],
            },
        ];

        public IEnumerator<object[]> GetEnumerator()
        {
            return Projects.Select(p => new object[] { p }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }*/
}
