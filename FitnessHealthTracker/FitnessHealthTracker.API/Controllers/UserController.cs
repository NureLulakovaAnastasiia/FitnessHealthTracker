using FitnessHealthTracker.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHealthTracker.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}/parameters")]
        public async Task<IActionResult> GetUserParameters(string userId)
        {
            var parameters = await _userService.GetUserParameters(userId);
            if (parameters.IsSuccess)
            {
                return Ok(parameters.Value);
            }
            return NotFound(parameters.Error);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult>  GetUserData(string userId)
        {
            var userData = await _userService.GetUser(userId);
            if (userData.IsSuccess)
            {
                return Ok(userData.Value);
            }
            return NotFound(userData.Error);
        }
    }
}
