using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TaskManagerWebApi.Models.Entities;

namespace Api.IntegrationTests.TestData
{
    public class ProjectsData : IEnumerable<object[]>
    {
        public static readonly List<ProjectEntity> Projects =
        [
            new() { Title = "title1", Description = "Descr1", Status = "New", 
                Start = DateTime.Now.AddDays(1), Deadline = DateTime.Now.AddDays(2), Creator = UsersData.Users[0]},
            new() { Title = "title2", Description = "Descr2", Status = "In Progress",
                Start = DateTime.Now.AddDays(1), Deadline = DateTime.Now.AddDays(4), Creator = UsersData.Users[0]},
            new() { Title = "title3", Description = "Descr3", Status = "In Progress",
                Start = DateTime.Now.AddDays(1), Deadline = DateTime.Now.AddDays(6), Creator = UsersData.Users[1]},
        ];

        public IEnumerator<object[]> GetEnumerator()
        {
            return Projects.Select(p => new object[] { p }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
