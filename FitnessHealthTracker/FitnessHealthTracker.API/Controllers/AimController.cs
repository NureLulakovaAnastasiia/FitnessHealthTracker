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
    public class AimController : ControllerBase
    {
        private readonly IAimService _aimService;

        public AimController(IAimService aimService)
        {
            _aimService = aimService;
        }
        /// <summary>
        /// Отримати список всіх цілей (системних)
        /// </summary>
        /// <returns>Список цілей</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAims()
        {
            var result = await _aimService.GetAllAims();
            if (result.IsSuccess) 
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Додавання нової мети
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost]
        public IActionResult AddAim([FromBody] Aim aim)
        {
            var result = _aimService.AddAim(aim);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Видалення мети
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("{aimId}")]
        public async Task<IActionResult> DeleteAim(int aimId)
        {
            var result = await _aimService.DeleteAim(aimId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення мети
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut]
        public IActionResult UpdateAim([FromBody] Aim aim)
        {
            var result = _aimService.UpdateAim(aim);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }


        /// <summary>
        /// Додавання нової мети користувача 
        /// </summary>
        /// <returns>Результат додавання (true/false)</returns>

        [HttpPost("user/add")]
        public IActionResult AddUserAim([FromBody] UserAimDto userAim)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized();
            }
            userAim.UserId = userId;
            var result = _aimService.AddUserAim(userAim);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }


        /// <summary>
        /// Видалення мети користувача
        /// </summary>
        /// <returns>Результат видалення (true/false)</returns>

        [HttpDelete("user/{userAimId}")]
        public async Task<IActionResult> DeleteUserAim(int userAimId)
        {
            var result = await _aimService.DeleteUserAim(userAimId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }


        /// <summary>
        /// Оновлення мети користувача
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPut("user/update")]
        public IActionResult UpdateUserAim([FromBody] UserAimDto userAim)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized();
            }
            userAim.UserId = userId;
            var result = _aimService.UpdateUserAim(userAim);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Отримання списку всіх цілей користувача
        /// </summary>
        /// <returns>Список цілей</returns>

        [HttpGet("user")]
        public async Task<IActionResult> GetAllUserAims(string? userId)
        {
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.Sid);
                if (userId == null)
                {
                    return Unauthorized();
                }
            }
            var result = await _aimService.GetAllUserAims(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Оновлення статусу мети на "досягнуто"
        /// </summary>
        /// <returns>Результат оновлення (true/false)</returns>

        [HttpPatch("user/achieve/{userAimId}")]
        public async Task<IActionResult> MarkUserAimAchieved(int userAimId)
        {
            var result = await _aimService.MarkUserAimAchieved(userAimId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        /// <summary>
        /// Отримання останньою за датою мети користувача
        /// </summary>
        /// <returns>Мета користувача</returns>

        [HttpGet("user/latest")]
        public async Task<IActionResult> GetLatestUserAim(string? userId)
        {
            if (userId == null)
            {
                var userClaimId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault();
                if (userClaimId == null)
                {
                    return Unauthorized();
                }
                userId = userClaimId.Value;
            }
            var result = await _aimService.GetLatestUserAim(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

    }
}
