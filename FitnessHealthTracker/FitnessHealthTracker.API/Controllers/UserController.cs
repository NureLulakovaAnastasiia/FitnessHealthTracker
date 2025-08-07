using FitnessHealthTracker.Application.DTOs;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Отримання параметрів користувача
        /// </summary>
        /// <returns>Параметри користувача</returns>

        [HttpGet("parameters/{userId}")]
        public async Task<IActionResult> GetUserParameters(string userId)
        {
            var parameters = await _userService.GetUserParameters(userId);
            if (parameters.IsSuccess)
            {
                return Ok(parameters.Value);
            }
            return NotFound(parameters.Error);
        }

        /// <summary>
        /// Оновлення параметрів користувача
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut("parameters")]
        public async Task<IActionResult> UpdateUserParameters([FromBody] UserParameters parameters)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var userData = User.Claims;
            var updateRes = await _userService.UpdateUserParameters(parameters, userData.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            if (updateRes.IsSuccess && updateRes.Value)
            {
                return Ok();
            }
            return StatusCode(500, updateRes.Error);
        }

        /// <summary>
        /// Отримання даних користувача
        /// </summary>
        /// <returns>Дані користувача</returns>

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

        /// <summary>
        /// Оновлення даних користувача
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut]
        public async Task<IActionResult> UpdateUserData([FromBody] GetUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateRes = await _userService.UpdateUser(userDto);
            if (updateRes.IsSuccess && updateRes.Value)
            {
                return Ok();
            }
            return StatusCode(500, updateRes.Error);
        }
    }
}
