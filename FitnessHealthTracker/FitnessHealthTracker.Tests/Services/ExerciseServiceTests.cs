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
    public class ExerciseServiceTests
    {
        private readonly Mock<IMealRepository> _exerciseRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<ExerciseService>> _loggerMock;
        private readonly ExerciseService _exerciseService;

        public ExerciseServiceTests()
        {
            _exerciseRepoMock = new Mock<IMealRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<ExerciseService>>();
            _exerciseService = new ExerciseService(_exerciseRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddExercise_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            var exercise = new Exercise("Test Exercise");
            _exerciseRepoMock.Setup(r => r.AddExercise(exercise)).Returns(true);

            var result = _exerciseService.AddExercise(exercise);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void AddExercise_ShouldReturnError_WhenRepositoryThrows()
        {
            var exercise = new Exercise("Test Aim");
            _exerciseRepoMock.Setup(r => r.AddExercise(exercise)).Throws(new System.Exception("DB error"));

            var result = _exerciseService.AddExercise(exercise);

            Assert.False(result.Value);
            Assert.Equal(Errors.AddingErrorMessage, result.Error);
        }

        [Fact]
        public void AddUserExercise_ShouldMapAndAddSuccessfully()
        {
            var dto = new UserExerciseDto
            {
                Id = 1,
                EndDate = new DateTime(2025, 8, 8),
                StartDate = new DateTime(2025, 8, 8),
                UserId = "user1",
                ExerciseId = 1
            };
            var entity = new UserExercise
            {
                Id = 1,
                EndDate = new DateTime(2025, 8, 8),
                StartDate = new DateTime(2025, 8, 8),
                UserId = "user1",
                ExerciseId = 1
            };

            _mapperMock.Setup(m => m.Map<UserExercise>(dto)).Returns(entity);
            _exerciseRepoMock.Setup(r => r.AddUserExercise(entity, dto.UserId)).Returns(true);

            var result = _exerciseService.AddUserExercise(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteExercise_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _exerciseRepoMock.Setup(r => r.DeleteExercise(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _exerciseService.RemoveExercise(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteExercise_ShouldReturnError_WhenRepositoryThrows()
        {
            _exerciseRepoMock.Setup(r => r.DeleteExercise(It.IsAny<int>())).ThrowsAsync(new System.Exception());

            var result = await _exerciseService.RemoveExercise(1);

            Assert.False(result.Value);
            Assert.Equal(Errors.DeletingErrorMessage, result.Error);
        }

        [Fact]
        public async Task DeleteUserExercise_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _exerciseRepoMock.Setup(r => r.DeleteUserExercise(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _exerciseService.RemoveUserExercise(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task GetAllExercises_ShouldReturnExercises_WhenRepositorySucceeds()
        {
            var exercises = new List<Exercise> { new Exercise("E1"), new Exercise("E2") };
            _exerciseRepoMock.Setup(r => r.GetAllExercises()).ReturnsAsync(exercises);

            var result = await _exerciseService.GetAvailableExercises();

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task GetAllUserExercises_ShouldReturnExercises_WhenRepositorySucceeds()
        {
            var exercises = new List<UserExercise> {
                new UserExercise() {Id = 1, ExerciseId = 1, Calories = 1, UserId = "user1"},
                new UserExercise() {Id = 1, ExerciseId = 1, Calories = 1, UserId = "user1"} };

            _exerciseRepoMock.Setup(r => r.GetAllUserExercises(It.IsAny<string>())).ReturnsAsync(exercises);

            var result = await _exerciseService.GetAllUserExercises("user1");

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }

        

        [Fact]
        public void UpdateExercise_ShouldUpdateSuccessfully()
        {
            var entity = new Exercise("E1");

            _exerciseRepoMock.Setup(r => r.UpdateExercise(entity)).Returns(true);

            var result = _exerciseService.UpdateExercise(entity);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }


        [Fact]
        public void UpdateUserExercise_ShouldMapAndUpdateSuccessfully()
        {
            var dto = new UserExerciseDto { Id = 1, ExerciseId = 1 };
            var entity = new UserExercise { Id = 1, ExerciseId = 1 };

            _mapperMock.Setup(m => m.Map<UserExercise>(dto)).Returns(entity);
            _exerciseRepoMock.Setup(r => r.UpdateUserExercise(entity)).Returns(true);

            var result = _exerciseService.UpdateUserExercise(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

    }
}
