using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;

namespace Api.IntegrationTests.TestData
{
    public class UsersData : IEnumerable<object[]>
    {
        public static readonly List<string> Passwords;
        public static readonly List<UserEntity> Users;

        static UsersData()
        {
            var hasher = new PasswordHasher<UserEntity>();

            var user1 = new UserEntity { FirstName = "Name1", Login = "login1" };
            var password1 = "123";
            user1.HashedPassword = hasher.HashPassword(user1, password1);

            var user2 = new UserEntity { FirstName = "Name2", Login = "login2" };
            var password2 = "456";
            user2.HashedPassword = hasher.HashPassword(user2, password2);

            var user3 = new UserEntity { FirstName = "Name3", Login = "login3" };
            var password3 = "789";
            user3.HashedPassword = hasher.HashPassword(user3, password3);

            Passwords = [password1, password2, password3];
            Users = [user1, user2, user3];
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return Users.Select(u => new object[] { u }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static UserRegisterRequest GetSimpleRegisterRequest()
        {
            return new UserRegisterRequest
            {
                FirstName = "name",
                Login = "login",
                Password = "password"
            };
        }
    }
}
