using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TaskManager.AuthService.DbConnection.Entities;
using TaskManager.AuthService.WebApi.Models;
using TaskManager.Core;
using Xunit;

namespace TaskManager.Tests.Unit.AuthService.Filters
{
    public class UsersFilterTests
    {
        private const int FirstUserId = 1;
        private const string FirstUserName = "johnblack";
        private const string FirstFirstName = "John";
        private const string FirstLastName = "Black";
        private const string FirstEmail = "johnblack@microsoft.com";

        private const int SecondUserId = 2;
        private const string SecondUserName = "laurensmith";
        private const string SecondFirstName = "Lauren";
        private const string SecondLastName = "Smith";
        private const string SecondEmail = "laurensmith@google.com";

        private const int ThirdUserId = 3;
        private const string ThirdUserName = "bendawson";
        private const string ThirdFirstName = "Benjamin";
        private const string ThirdLastName = "Dawson";
        private const string ThirdEmail = "bendawson@yahoo.com";

        private readonly IQueryable<IUser> _users;

        public UsersFilterTests()
        {
            _users = new List<IUser>
            {
                new UserEntity
                {
                    Id = FirstUserId,
                    Username = FirstUserName,
                    FirstName = FirstFirstName,
                    LastName = FirstLastName,
                    Email = FirstEmail
                },
                new UserEntity
                {
                    Id = SecondUserId,
                    Username = SecondUserName,
                    FirstName = SecondFirstName,
                    LastName = SecondLastName,
                    Email = SecondEmail
                },
                new UserEntity
                {
                    Id = ThirdUserId,
                    Username = ThirdUserName,
                    FirstName = ThirdFirstName,
                    LastName = ThirdLastName,
                    Email = ThirdEmail
                }
            }.AsQueryable();
        }


        [Theory]
        [InlineData(null, new[] { FirstUserId, SecondUserId, ThirdUserId })]
        [InlineData("yahoo.com", new[] { ThirdUserId })]
        [InlineData("laurensmith", new[] { SecondUserId })]
        [InlineData("Black", new[] { FirstUserId })]
        [InlineData("en", new[] { SecondUserId, ThirdUserId })]
        [InlineData("randomString", new int[] { })]
        public void FilterByValueTest(string value, int[] userIds)
        {
            var filter = new UsersFilter { Value = value };
            var result = filter.Filter(_users).ToList();
            AssertOnlyConatainsItems(result, userIds);
        }

        private void AssertOnlyConatainsItems(List<IUser> users, int[] userIds)
        {
            users.Should().HaveCount(userIds.Length);

            userIds.ToList().ForEach(x =>
            {
                users.Should().Contain(y => y.Id == x);
            });
        }
    }
}