using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessHealthTracker.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthParametersController : ControllerBase
    {
        private readonly IHealthParametersService _parameterService;

        public HealthParametersController(IHealthParametersService parameterService)
        {
            _parameterService = parameterService;
        }


        /// <summary>
        /// Додавання нової показника здоров'я
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost]
        public IActionResult AddParameter([FromBody] HealthParameter parameter)
        {
            if (parameter.UserId == null)
            {
                var userId = User.FindFirstValue(ClaimTypes.Sid);
                if (userId == null)
                {
                    return Unauthorized();
                }
                parameter.UserId = userId;
            }
            var result = _parameterService.AddParameter(parameter);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Видалення показника здоров'я
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("{parameterId}")]
        public async Task<IActionResult> RemoveParameter(int parameterId)
        {
            var result = await _parameterService.RemoveParameter(parameterId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }


        /// <summary>
        /// Отримання списку всіх показників користувача за типом показника
        /// </summary>
        /// <returns>Список показників</returns>

        [HttpGet("type")]
        public async Task<IActionResult> GetParametersByType([FromQuery] HealthParameterType type, [FromQuery] string? userId)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.Sid);
                if (userId == null)
                {
                    return Unauthorized();
                }
            }
            var result = await _parameterService.GetParametersByType(type, userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Отримання списку всіх показників користувача за певний інтервал часу
        /// </summary>
        /// <returns>Список показників</returns>

        [HttpGet("interval")]
        public async Task<IActionResult> GetParametersInTimeInterval(
            [FromQuery] DateTime? start,
            [FromQuery] DateTime? end,
            [FromQuery] string? userId)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.Sid);
                if (userId == null)
                {
                    return Unauthorized();
                }
            }

            var result = await _parameterService.GetParametersInTimeInterval(start, end, userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

    }
}
