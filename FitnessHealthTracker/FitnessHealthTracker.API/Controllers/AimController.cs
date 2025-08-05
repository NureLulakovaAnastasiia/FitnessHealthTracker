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
    public class AimController : ControllerBase
    {
        private readonly IAimService _aimService;

        public AimController(IAimService aimService)
        {
            _aimService = aimService;
        }

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

        [HttpPost("user")]
        public IActionResult AddUserAim([FromBody] UserAimDto userAim)
        {
            var result = _aimService.AddUserAim(userAim);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

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

        [HttpPut("user")]
        public IActionResult UpdateUserAim([FromBody] UserAimDto userAim)
        {
            var result = _aimService.UpdateUserAim(userAim);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllUserAims(string userId)
        {
            var result = await _aimService.GetAllUserAims(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

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

        [HttpGet("user/latest/{userId}")]
        public async Task<IActionResult> GetLatestUserAim(string userId)
        {
            var result = await _aimService.GetLatestUserAim(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return StatusCode(500, result.Error);
        }

    }
}
