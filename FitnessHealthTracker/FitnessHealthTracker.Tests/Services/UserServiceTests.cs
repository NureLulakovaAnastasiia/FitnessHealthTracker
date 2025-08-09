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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUser_ShouldReturnGetUserDto_WhenRepositorySucceeds()
        {
            var entity = new User { Id = "user1", Email = "email", FirstName = "first", LastName = "last" };
            var dto = new GetUserDto { Id = "user1", Email = "email", FirstName = "first", LastName = "last" };
            _userRepoMock.Setup(r => r.GetUserById(It.IsAny<string>())).ReturnsAsync(entity);
            
            _mapperMock.Setup(m => m.Map<GetUserDto>(entity)).Returns(dto);
            var result = await _userService.GetUser("user1");

            Assert.NotNull(result.Value);
            Assert.Null(result.Error);
        }
        [Fact]
        public async Task GetUserParameters_ShouldReturnUserParameters_WhenRepositorySucceeds()
        {
            var userParameters = new UserParameters() { Id = 1, DateOfBirthday = new DateTime(2025, 8, 8) };
            _userRepoMock.Setup(r => r.GetUserParameters(It.IsAny<string>()))
                .ReturnsAsync(new Result<UserParameters> { Value = userParameters });

            var result = await _userService.GetUserParameters("user1");

            Assert.NotNull(result.Value);
            Assert.Null(result.Error);
        }
        
        [Fact]
        public async Task UpdateUser_ShouldUpdateSuccessfully()
        {
            var entity = new User { Id = "user1", Email = "email", FirstName = "first", LastName = "last" };
            var dto = new GetUserDto { Id = "user1", Email = "email2", FirstName = "first2", LastName = "last2" };

            _userRepoMock.Setup(r => r.GetUserById(It.IsAny<string>())).ReturnsAsync(entity);
            _userRepoMock.Setup(r => r.UpdateUser(It.IsAny<User>())).Returns(true);

            var result = await _userService.UpdateUser(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task UpdateUserParameters_ShouldUpdateSuccessfully()
        {
            var entity = new User { Id = "user1", Email = "email", FirstName = "first", LastName = "last" };
            var parameters = new UserParameters() { Id = 1, DateOfBirthday = new DateTime(2025, 8, 8) };


            _userRepoMock.Setup(r => r.GetUserById(It.IsAny<string>())).ReturnsAsync(entity);
            _userRepoMock.Setup(r => r.GetUserParameters(It.IsAny<string>()))
                .ReturnsAsync(new Result<UserParameters> { Value = parameters });
            _userRepoMock.Setup(r => r.UpdateUserParameters(It.IsAny<UserParameters>())).Returns(true);

            var result = await _userService.UpdateUserParameters(parameters, "user1");

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }
    }
}
