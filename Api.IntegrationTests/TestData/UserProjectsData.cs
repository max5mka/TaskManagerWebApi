using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Entities;

namespace Api.IntegrationTests.TestData
{
    /*public class UserProjectsData : IEnumerable<object[]>
    {
        public static readonly List<UserProject> UserProjects =
        [
            new() { 
                UserId = UsersData.Users[0].Id, 
                User = UsersData.Users[0],
                Project = ProjectsData.Projects[0],
                ProjectRole = ProjectRoles.Owner 
            },
            new() {
                UserId = UsersData.Users[0].Id,
                User = UsersData.Users[0],
                Project = ProjectsData.Projects[1],
                ProjectRole = ProjectRoles.Owner
            },
            new() {
                UserId = UsersData.Users[1].Id,
                User = UsersData.Users[1],
                Project = ProjectsData.Projects[2],
                ProjectRole = ProjectRoles.Owner
            },
        ];

        public IEnumerator<object[]> GetEnumerator()
        {
            return UserProjects.Select(up => new object[] { up }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }*/
}
