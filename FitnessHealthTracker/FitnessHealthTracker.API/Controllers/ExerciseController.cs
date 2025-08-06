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
