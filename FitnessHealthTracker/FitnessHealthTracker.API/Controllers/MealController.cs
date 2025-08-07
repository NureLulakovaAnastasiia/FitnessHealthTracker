using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHealthTracker.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        /// <summary>
        /// Отримання списку всіх доступних страв (системних)
        /// </summary>
        /// <returns>Список страв</returns>

        [HttpGet]
        public async Task<IActionResult> GetMeals()
        {
            var result = await _mealService.GetMeals();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Отримання списку всіх страв, створених користувачем
        /// </summary>
        /// <returns>Список страв</returns>

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserMeals(string userId)
        {
            var result = await _mealService.GetUserMeals(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Додавання нової страви
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost]
        public IActionResult AddMeal([FromBody] Meal meal)
        {
            var result = _mealService.AddNewMeal(meal);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення страви
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut]
        public IActionResult UpdateMeal([FromBody] UpdateMealDto mealDto)
        {
            var result = _mealService.UpdateMeal(mealDto);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення поживних речовин страви
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPatch("{mealId}/nutrients")]
        public IActionResult UpdateMealNutrients(int mealId, [FromBody] MealNutrients nutrients)
        {
            var result = _mealService.UpdateMealNutrients(mealId, nutrients);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Видалення страви
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("{mealId}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            var result = await _mealService.DeleteMeal(mealId);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Отримання списку всіх прийомів їжі користувачем
        /// </summary>
        /// <returns>Список прийомів їжі</returns>

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetMealsHistory(string userId)
        {
            var result = await _mealService.GetMealsHistory(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Додавання нового прийому їжі (страви) користувачем
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost("history")]
        public IActionResult AddMealHistory([FromBody] MealHistoryDto history)
        {
            var result = _mealService.AddMealHistory(history);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення прийому їжі
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut("history")]
        public IActionResult UpdateMealHistory([FromBody] MealHistoryDto history)
        {
            var result = _mealService.UpdateMealHistory(history);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Видалення прийому їжі
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("history/{historyId}")]
        public async Task<IActionResult> DeleteMealHistory(int historyId)
        {
            var result = await _mealService.DeleteMealHistory(historyId);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, result.Error);
        }

    }
}
