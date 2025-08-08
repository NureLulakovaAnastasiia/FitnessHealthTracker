using AutoMapper;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.Service;
using FitnessHealthTracker.Domain.Entities;
using FitnessHealthTracker.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Tests.Services
{
    public class HealthParameterServiceTests
    {
        private readonly Mock<IHealthParametersRepository> _healthParamRepoMock;
        private readonly Mock<ILogger<HealthParameterService>> _loggerMock;
        private readonly HealthParameterService _healthParamService;

        public HealthParameterServiceTests()
        {
            _healthParamRepoMock = new Mock<IHealthParametersRepository>();
            _loggerMock = new Mock<ILogger<HealthParameterService>>();
            _healthParamService = new HealthParameterService(_healthParamRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddParameter_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            var parameter = new HealthParameter() { Value = 1, DateTime = new DateTime(2025, 8,8), Type = HealthParameterType.Weight};
            _healthParamRepoMock.Setup(r => r.AddParameter(parameter)).Returns(true);

            var result = _healthParamService.AddParameter(parameter);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void AddParameter_ShouldReturnError_WhenRepositoryThrows()
        {
            var parameter = new HealthParameter() { Value = 1, DateTime = new DateTime(2025, 8, 8), Type = HealthParameterType.Weight };
            _healthParamRepoMock.Setup(r => r.AddParameter(parameter)).Throws(new System.Exception("DB error"));

            var result = _healthParamService.AddParameter(parameter);

            Assert.False(result.Value);
            Assert.Equal(Errors.AddingErrorMessage, result.Error);
        }

        [Fact]
        public async Task GetParametersByType_ShouldReturnParameters_WhenRepositorySucceeds()
        {
            var parameters = new List<HealthParameter> { 
                new HealthParameter() { Value = 1, DateTime = new DateTime(2025, 8, 8), Type = HealthParameterType.Weight },
                new HealthParameter() { Value = 1, DateTime = new DateTime(2025, 9, 8), Type = HealthParameterType.Weight } 
            };
            _healthParamRepoMock.Setup(r => r.GetParametersByType(It.IsAny<HealthParameterType>(), It.IsAny<string>()))
                .ReturnsAsync(parameters);

            var result = await _healthParamService.GetParametersByType(HealthParameterType.Weight, "user");

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task GetParametersByInTimeInterval_ShouldReturnParameters_WhenRepositorySucceeds()
        {
            var parameters = new List<HealthParameter> {
                new HealthParameter() { Value = 1, DateTime = new DateTime(2025, 8, 8), Type = HealthParameterType.Weight },
                new HealthParameter() { Value = 1, DateTime = new DateTime(2025, 9, 8), Type = HealthParameterType.Weight }
            };
            _healthParamRepoMock.Setup(r => r.GetParametersInTimeInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(parameters);

            var result = await _healthParamService.GetParametersInTimeInterval(new DateTime(2025, 8,8), new DateTime(2025, 9, 8), "user");

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }


        [Fact]
        public async Task RemoveParameter_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _healthParamRepoMock.Setup(r => r.RemoveParameter(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _healthParamService.RemoveParameter(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task RemoveParameter_ShouldReturnError_WhenRepositoryThrows()
        {
            _healthParamRepoMock.Setup(r => r.RemoveParameter(It.IsAny<int>())).ThrowsAsync(new System.Exception());

            var result = await _healthParamService.RemoveParameter(1);

            Assert.False(result.Value);
            Assert.Equal(Errors.DeletingErrorMessage, result.Error);
        }

    }
}
