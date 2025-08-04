using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessHealthTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(NewUserDto userDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var res = await _authService.Register(userDto);
            if (res == null)
            {
                return Ok();
            }

            return StatusCode(500, res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _authService.LogIn(userDto);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }

            return StatusCode(500, res.Error);
        }

       
    }
}
