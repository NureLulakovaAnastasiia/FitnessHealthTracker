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
    public class HealthParametersController : ControllerBase
    {
        private readonly IHealthParametersService _parameterService;

        public HealthParametersController(IHealthParametersService parameterService)
        {
            _parameterService = parameterService;
        }

        [HttpPost]
        public IActionResult AddParameter([FromBody] HealthParameter parameter)
        {
            var result = _parameterService.AddParameter(parameter);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

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

        [HttpGet("type")]
        public async Task<IActionResult> GetParametersByType([FromQuery] HealthParameterType type, [FromQuery] string userId)
        {
            var result = await _parameterService.GetParametersByType(type, userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        [HttpGet("interval")]
        public async Task<IActionResult> GetParametersInTimeInterval(
            [FromQuery] DateTime? start,
            [FromQuery] DateTime? end,
            [FromQuery] string userId)
        {
            var result = await _parameterService.GetParametersInTimeInterval(start, end, userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

    }
}
