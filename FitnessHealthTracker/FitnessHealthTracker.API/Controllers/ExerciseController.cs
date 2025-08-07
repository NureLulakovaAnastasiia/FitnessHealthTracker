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
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }


        /// <summary>
        /// Отримання список всіх доступних вправ (системних)
        /// </summary>
        /// <returns>Список вправ</returns>

        [HttpGet("all")]
        public async Task<IActionResult> GetAvailableExercises()
        {
            var result = await _exerciseService.GetAvailableExercises();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }
        /// <summary>
        /// Додавання нової вправи
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost]
        public IActionResult AddExercise([FromBody] Exercise exercise)
        {
            var result = _exerciseService.AddExercise(exercise);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Видалення вправи
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveExercise(int exerciseId)
        {
            var result = await _exerciseService.RemoveExercise(exerciseId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення вправи
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut]
        public IActionResult UpdateExercise([FromBody] Exercise exercise)
        {
            var result = _exerciseService.UpdateExercise(exercise);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }


        /// <summary>
        /// Додавання нової вправи користувача (з тривалістю та витраченими калоріями)
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost("user")]
        public IActionResult AddUserExercise([FromBody] UserExerciseDto exercise)
        {
            var result = _exerciseService.AddUserExercise(exercise);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення вправи користувача
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut("user")]
        public IActionResult UpdateUserExercise([FromBody] UserExerciseDto exercise)
        {
            var result = _exerciseService.UpdateUserExercise(exercise);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Видалення вправи користувача
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("user/{exerciseId}")]
        public async Task<IActionResult> RemoveUserExercise(int exerciseId)
        {
            var result = await _exerciseService.RemoveUserExercise(exerciseId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }


        /// <summary>
        /// Отримання список всіх вправ користувача
        /// </summary>
        /// <returns>Список вправ</returns>

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllUserExercises(string userId)
        {
            var result = await _exerciseService.GetAllUserExercises(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

    }
}
