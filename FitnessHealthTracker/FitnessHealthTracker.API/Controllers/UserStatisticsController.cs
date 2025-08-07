using FitnessHealthTracker.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHealthTracker.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : ControllerBase
    {
        private readonly IUserStatisticsService _userStatisticsService;

        public UserStatisticsController(IUserStatisticsService userStatisticsService)
        {
            _userStatisticsService = userStatisticsService;
        }

        [HttpGet("TotalCaloriesPerMealDate")]
        public async Task<IActionResult> GetTotalCaloriesPerMealDate([FromQuery] DateTime start,
            [FromQuery] DateTime? end,
            [FromQuery] string userId)
        {
            var res = await _userStatisticsService.GetTotalCaloriesPerMealDate(start, end, userId);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Error);
        }

        [HttpGet("TotalCaloriesPerActivityDate")]
        public async Task<IActionResult> GetTotalCaloriesPerActivityDate([FromQuery] DateTime start,
            [FromQuery] DateTime? end,
            [FromQuery] string userId)
        {
            var res = await _userStatisticsService.GetTotalCaloriesPerActivityDate(start, end, userId);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Error);
        }

        [HttpGet("TotalCaloriesPerDate")]
        public async Task<IActionResult> GetTotalCaloriesPerDayDate([FromQuery] DateTime start,
        [FromQuery] DateTime? end,
        [FromQuery] string userId)
        {
            var res = await _userStatisticsService.GetTotalCaloriesPerDate(start, end, userId);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Error);
        }

    }
}
