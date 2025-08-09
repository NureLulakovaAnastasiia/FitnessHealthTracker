
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.Service;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TokenService _tokenService;
        public TokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _tokenService = new TokenService(_configurationMock.Object);
        }

        [Fact]
        public void GenerateToken_ShouldReturnToken_WhenTokenIsSuccessfullyGenerated()
        {
            _configurationMock.Setup(c => c["JWT:SigningKey"])
                    .Returns("test-signing-key-test-signing-key");

            var user = new User() { Id = "1", Email = "email", FirstName = "first", LastName = "last" };

            var result = _tokenService.GenerateToken(user);
            Assert.NotNull(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void GenerateToken_ShouldReturnError_WhenMethodThrows()
        {
            _configurationMock.Setup(c => c["JWT:SigningKey"])
                    .Returns("test-signing-key");

            var user = new User() { Id = "1", Email = "email", FirstName = "first", LastName = "last" };

            var result = _tokenService.GenerateToken(user);
            Assert.Null(result.Value);
            Assert.NotNull(result.Error);
        }
    }
}
