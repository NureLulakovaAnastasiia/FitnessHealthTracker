using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
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
    public class AimServiceTests
    {
        private readonly Mock<IAimRepository> _aimRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<AimService>> _loggerMock;
        private readonly AimService _aimService;

        public AimServiceTests()
        {
            _aimRepoMock = new Mock<IAimRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<AimService>>();
            _aimService = new AimService(_aimRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddAim_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            var aim = new Aim("Test Aim");
            _aimRepoMock.Setup(r => r.AddAim(aim)).Returns(true);

            var result = _aimService.AddAim(aim);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void AddAim_ShouldReturnError_WhenRepositoryThrows()
        {
            var aim = new Aim("Test Aim");
            _aimRepoMock.Setup(r => r.AddAim(aim)).Throws(new System.Exception("DB error"));

            var result = _aimService.AddAim(aim);

            Assert.False(result.Value);
            Assert.Equal(Errors.AddingErrorMessage, result.Error);
        }

        [Fact]
        public void AddUserAim_ShouldMapAndAddSuccessfully()
        {
            var dto = new UserAimDto
            {
                Id = 1,
                EndDate = new DateTime(2025, 8, 8),
                StartDate = new DateTime(2025, 8, 8),
                UserId = "user1",
                AimId = 1
            };
            var entity = new UserAim
            {
                Id = 1,
                EndDate = new DateTime(2025, 8, 8),
                StartDate = new DateTime(2025, 8, 8),
                AimId = 1
            };

            _mapperMock.Setup(m => m.Map<UserAim>(dto)).Returns(entity);
            _aimRepoMock.Setup(r => r.AddUserAim(entity, dto.UserId)).Returns(true);

            var result = _aimService.AddUserAim(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteAim_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _aimRepoMock.Setup(r => r.DeleteAim(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _aimService.DeleteAim(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteAim_ShouldReturnError_WhenRepositoryThrows()
        {
            _aimRepoMock.Setup(r => r.DeleteAim(It.IsAny<int>())).ThrowsAsync(new System.Exception());

            var result = await _aimService.DeleteAim(1);

            Assert.False(result.Value);
            Assert.Equal(Errors.DeletingErrorMessage, result.Error);
        }
        
        [Fact]
        public async Task DeleteUserAim_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _aimRepoMock.Setup(r => r.DeleteUserAim(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _aimService.DeleteUserAim(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task GetAllAims_ShouldReturnAims_WhenRepositorySucceeds()
        {
            var aims = new List<Aim> { new Aim("A1"), new Aim("A2") };
            _aimRepoMock.Setup(r => r.GetAllAims()).ReturnsAsync(aims);

            var result = await _aimService.GetAllAims();

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }
        
        [Fact]
        public async Task GetAllUserAims_ShouldReturnAims_WhenRepositorySucceeds()
        {
            var aims = new List<UserAim> { 
                new UserAim(new Aim("A1"), new DateTime(2025, 8,8), 8),
                new UserAim(new Aim("A2"), new DateTime(2025, 8,8), 8)};

            _aimRepoMock.Setup(r => r.GetAllUserAims(It.IsAny<string>())).ReturnsAsync(aims);

            var result = await _aimService.GetAllUserAims("user1");

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task GetLatestUserAim_ShouldReturnMostRecentAim()
        {
            var aims = new List<UserAim>
        {
            new UserAim { StartDate = new DateTime(2025, 1, 1) },
            new UserAim { StartDate = new DateTime(2025, 5, 1) }
        };

            _aimRepoMock.Setup(r => r.GetAllUserAims("user1")).ReturnsAsync(aims);

            var result = await _aimService.GetLatestUserAim("user1");

            Assert.Equal(new DateTime(2025, 5, 1), result.Value.StartDate);
        }

        [Fact]
        public async Task MarkUserAimAchieved_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _aimRepoMock.Setup(r => r.MarkUserAimAchieved(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _aimService.MarkUserAimAchieved(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void UpdateAim_ShouldMapAndUpdateSuccessfully()
        {
            var entity = new Aim("Aim1");

            _aimRepoMock.Setup(r => r.UpdateAim(entity)).Returns(true);

            var result = _aimService.UpdateAim(entity);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }


        [Fact]
        public void UpdateUserAim_ShouldMapAndUpdateSuccessfully()
        {
            var dto = new UserAimDto { Id = 1, AimId = 1 };
            var entity = new UserAim { Id = 1, AimId = 1 };

            _mapperMock.Setup(m => m.Map<UserAim>(dto)).Returns(entity);
            _aimRepoMock.Setup(r => r.UpdateUserAim(entity)).Returns(true);

            var result = _aimService.UpdateUserAim(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

    }

}

