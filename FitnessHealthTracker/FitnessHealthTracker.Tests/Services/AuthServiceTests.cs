using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Application.Service;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<ITokenService> _tokenServMock;
        private readonly Mock<ILogger<AuthService>> _loggerMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _authRepoMock = new Mock<IAuthRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _tokenServMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger<AuthService>>();
            _authService = new AuthService(_authRepoMock.Object, _tokenServMock.Object, _userRepoMock.Object, _loggerMock.Object);
        }


        //check all possible errors in logIn function
        [Fact]
        public async Task LogIn_ShouldReturnError_WhenRepositoryThrowsInLogIn()
        {
            _authRepoMock.Setup(r => r.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool> { Value = false, Error = Errors.LoginErrorMessage });

            var user = new UserLoginDto() { Email = "email", Password = "password" };

            var result = await _authService.LogIn(user);
            
            Assert.Null(result.Value);
            Assert.Equal(Errors.LoginErrorMessage, result.Error);
        }

        [Fact]
        public async Task LogIn_ShouldReturnError_WhenRepositoryThrowsInGettingUserByEmail()
        {
            _authRepoMock.Setup(r => r.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool> { Value = true });
            _userRepoMock.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var user = new UserLoginDto() { Email = "email", Password = "password" };

            var result = await _authService.LogIn(user);

            Assert.Null(result.Value);
            Assert.Equal(Errors.LoginErrorMessage, result.Error);
        }

        [Fact]
        public async Task LogIn_ShouldReturnError_WhenServiceThrowsInGeneratingToken()
        {
            _authRepoMock.Setup(r => r.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool> { Value = true });
            _userRepoMock.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new User("first", "last", "email"));
            _tokenServMock.Setup(r => r.GenerateToken(It.IsAny<User>())).Returns((Result<string>?)null);

            var user = new UserLoginDto() { Email = "email", Password = "password" };

            var result = await _authService.LogIn(user);

            Assert.Null(result.Value);
            Assert.Equal(Errors.LoginErrorMessage, result.Error);
        }

        [Fact]
        public async Task LogIn_ShouldReturnError_WhenServiceThrows()
        {
            _authRepoMock.Setup(r => r.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new System.Exception());

            var user = new UserLoginDto() { Email = "email", Password = "password" };

            var result = await _authService.LogIn(user);

            Assert.Null(result.Value);
            Assert.Equal(Errors.LoginErrorMessage, result.Error);
        }

        [Fact]
        public async Task LogIn_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _authRepoMock.Setup(r => r.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool> { Value = true });
            _userRepoMock.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new User("first", "last", "email"));
            _tokenServMock.Setup(r => r.GenerateToken(It.IsAny<User>())).Returns(new Result<string> { Value = "token"});

            var user = new UserLoginDto() { Email = "email", Password = "password" };

            var result = await _authService.LogIn(user);

            Assert.NotEmpty(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _authRepoMock.Setup(r => r.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool> {Value = true});

            var user = new NewUserDto()
            {
                Email = "email",
                Password = "password",
                FirstName = "first",
                LastName = "last"
            };
            var result = await _authService.Register(user);

            Assert.Null(result);
        }


        [Fact]
        public async Task Register_ShouldReturnError_WhenRepositoryThrows()
        {
            _authRepoMock.Setup(r => r.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool> { Error = Errors.UserAlreadyExistsMessage });

            var user = new NewUserDto()
            {
                Email = "email",
                Password = "password",
                FirstName = "first",
                LastName = "last"
            };
            var result = await _authService.Register(user);

            Assert.NotNull(result);
        }
    }
}
