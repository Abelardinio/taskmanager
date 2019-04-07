using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TaskManager.AuthService.Data;
using TaskManager.AuthService.Data.DataProviders;
using TaskManager.AuthService.DbConnection.Entities;
using TaskManager.AuthService.DbConnection.Models;
using TaskManager.Common.Resources;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Exceptions;
using Xunit;

namespace TaskManager.Tests.Unit.AuthService.DataProviders
{
    public class LoginDataProviderTests
    {
        private readonly Mock<IUsersDataAccessor> _usersDataAccessorMock = new Mock<IUsersDataAccessor>();
        private readonly Mock<IHashCreator> _hashCreatorMock = new Mock<IHashCreator>();
        private readonly Mock<ITokenProvider> _tokenProviderMock = new Mock<ITokenProvider>();
        private readonly ILoginDataProvider _loginDataProvider;
        private const string Username = "Username";
        private const string Password = "Password";
        private const string PasswordHash = "PasswordHash";
        private const string PasswordSalt = "PasswordSalt";
        private const string WrongUsername = "WrongUsername";
        private const string WrongPassword = "WrongPassword";
        private const string Token = "Token";

        public LoginDataProviderTests()
        {
            var user = new UserLoginInfoModel { Username = Username, Password = PasswordHash, PasswordSalt = PasswordSalt};

            _usersDataAccessorMock.Setup(x => x.GetLoginInfo()).Returns(Extensions
                .GetDbSetMock(new List<UserLoginInfoModel> {user}.AsQueryable()).Object);

            _hashCreatorMock.Setup(x => x.Create(Username, Password, PasswordSalt)).Returns(PasswordHash);
            _tokenProviderMock.Setup(x => x.Get(user)).Returns(Token);

            _loginDataProvider = new LoginDataProvider(_usersDataAccessorMock.Object, _hashCreatorMock.Object, _tokenProviderMock.Object);
        }

        [Fact]
        public void ShouldThrowAuthExceptionIfWrongUserName()
        {
            Action action = () => _loginDataProvider.AuthenticateAsync(WrongUsername, Password).Wait();
            action.Should().Throw<AuthException>().WithMessage(ErrorMessages.Login_AuthError);
        }

        [Fact]
        public void ShouldThrowAuthExceptionIfWrongPassword()
        {
            Action action = () => _loginDataProvider.AuthenticateAsync(Username, WrongPassword).Wait();
            action.Should().Throw<AuthException>().WithMessage(ErrorMessages.Login_AuthError);
        }

        [Fact]
        public async Task ShouldReturnToken()
        {
            var token = await _loginDataProvider.AuthenticateAsync(Username, Password);
            token.Should().BeEquivalentTo(Token);
        }
    }
}