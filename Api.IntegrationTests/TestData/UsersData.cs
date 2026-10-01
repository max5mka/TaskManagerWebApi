using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TaskManagerWebApi.Models.Entities;

namespace Api.IntegrationTests.TestData
{
    public class UsersData : IEnumerable<object[]>
    {
        public static readonly List<UserEntity> Users =
        [
            new() { FirstName = "Name1", Login = "login1", HashedPassword = "123" },
            new() { FirstName = "Name2", Login = "login2", HashedPassword = "456" },
        ];

        public IEnumerator<object[]> GetEnumerator()
        {
            return Users.Select(u => new object[] { u }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
