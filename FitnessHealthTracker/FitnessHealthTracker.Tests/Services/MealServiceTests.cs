using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
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
    public class MealServiceTests
    {
        private readonly Mock<IMealRepository> _mealRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<MealService>> _loggerMock;
        private readonly MealService _mealService;

        public MealServiceTests()
        {
            _mealRepoMock = new Mock<IMealRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<MealService>>();
            _mealService = new MealService(_mealRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddMeal_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            var meal = new Meal(1, "meal", new MealNutrients());
            _mealRepoMock.Setup(r => r.AddMeal(meal)).Returns(true);

            var result = _mealService.AddNewMeal(meal);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void AddMealHistory_ShouldMapAndAddSuccessfully()
        {
            var dto = new MealHistoryDto
            {
                Id = 1,
                UserId = "user1",
                MealId = 1
            };
            var entity = new MealHistory
            {
                Id = 1,
                UserId = "user1",
                MealId = 1
            };

            _mapperMock.Setup(m => m.Map<MealHistory>(dto)).Returns(entity);
            _mealRepoMock.Setup(r => r.AddMealHistory(entity, dto.UserId)).Returns(true);

            var result = _mealService.AddMealHistory(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteMeal_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _mealRepoMock.Setup(r => r.DeleteMeal(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _mealService.DeleteMeal(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteMeal_ShouldReturnError_WhenRepositoryThrows()
        {
            _mealRepoMock.Setup(r => r.DeleteMeal(It.IsAny<int>())).ThrowsAsync(new System.Exception());

            var result = await _mealService.DeleteMeal(1);

            Assert.False(result.Value);
            Assert.Equal(Errors.DeletingErrorMessage, result.Error);
        }

        [Fact]
        public async Task DeleteMealHistory_ShouldReturnSuccess_WhenRepositorySucceeds()
        {
            _mealRepoMock.Setup(r => r.DeleteMealHistory(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _mealService.DeleteMealHistory(1);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task GetMeals_ShouldReturnMeals_WhenRepositorySucceeds()
        {
            var meals = new List<Meal> { new Meal(), new Meal() };
            _mealRepoMock.Setup(r => r.GetAllMeals()).ReturnsAsync(meals);

            var result = await _mealService.GetMeals();

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task GetMealsHistory_ShouldReturnMealsHistories_WhenRepositorySucceeds()
        {
            var meals = new List<MealHistory> {
               new MealHistory() {MealId = 1, UserId = "user1" , WeightInGrams = 1, Date = new DateTime(2025, 8,8)},
               new MealHistory() {MealId = 1, UserId = "user1" , WeightInGrams = 1, Date = new DateTime(2025, 8,8)}};

            _mealRepoMock.Setup(r => r.GetAllUserMealHistory(It.IsAny<string>())).ReturnsAsync(meals);

            var result = await _mealService.GetMealsHistory("user1");

            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task GetUserMeals_ShouldReturnMeals_WhenRepositorySucceeds()
        {
            var meals = new List<Meal> { new Meal(1, "meal1", new MealNutrients()) { UserId = "user1"} , new Meal(1, "meal2", new MealNutrients() )};
            _mealRepoMock.Setup(r => r.GetAllMeals()).ReturnsAsync(meals);

            var result = await _mealService.GetUserMeals("user1");

            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Count);

        }

        [Fact]
        public void UpdateMeal_ShouldUpdateSuccessfully()
        {
            var dto = new UpdateMealDto() {Id = 1, Name = "meal" };
            var entity = new Meal(1, "meal", new MealNutrients());

            _mapperMock.Setup(m => m.Map<Meal>(dto)).Returns(entity);
            _mealRepoMock.Setup(r => r.UpdateMeal(entity)).Returns(true);

            var result = _mealService.UpdateMeal(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void UpdateMealHistory_ShouldMapAndUpdateSuccessfully()
        {
            var dto = new MealHistoryDto { Id = 1, MealId = 1, UserId = "user1", WeightInGrams = 1 };
            var entity = new MealHistory { Id = 1, MealId = 1, UserId = "user1", WeightInGrams = 1 };

            _mapperMock.Setup(m => m.Map<MealHistory>(dto)).Returns(entity);
            _mealRepoMock.Setup(r => r.UpdateMealHistory(entity, It.IsAny<string>())).Returns(true);

            var result = _mealService.UpdateMealHistory(dto);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }
        [Fact]
        public void UpdateMealNutrients_ShouldMapAndUpdateSuccessfully()
        {
            var entity = new MealNutrients { Id = 1, CaloriesPerHundredGrams = 1, CarbohydratesPerHundredGrams = 1, FatsPerHundredGrams = 1, ProteinsPerHundredGrams = 1};

            _mealRepoMock.Setup(r => r.UpdateMealNutrients(entity, It.IsAny<int>())).Returns(true);

            var result = _mealService.UpdateMealNutrients(1, entity);

            Assert.True(result.Value);
            Assert.Null(result.Error);
        }

    }
}
