using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;


        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IUserRepository userRepository, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<string>> LogIn(UserLoginDto userLoginDto)
        {
            
            var result = new Result<string>();

            try
            {
                var loginResult = await _authRepository.LogIn(userLoginDto.Email, userLoginDto.Password);
                if (loginResult == null || !loginResult.IsSuccess)
                {
                    result.Error = loginResult == null ? Errors.LoginErrorMessage : loginResult.Error;
                    Log.Error("Error during login by user '{Email}'", userLoginDto.Email);
                    return result;
                }

                var user = await _userRepository.GetUserByEmail(userLoginDto.Email);
                if (user == null)
                {
                    result.Error = Errors.LoginErrorMessage;
                    Log.Error("Error during login by user '{Email}'", userLoginDto.Email);

                }
                else
                {
                    var token = _tokenService.GenerateToken(user);
                    if (token == null)
                    {
                        result.Error = Errors.LoginErrorMessage;
                        Log.Error("Error during login by user '{Id}' in generating token", user.Id);

                    }
                    else
                    {
                        result.Value = token.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                Log.ForContext("Email", userLoginDto.Email)
                    .Error(ex, "Error during login");

            }

            return result;
        }

        public async Task<string?> Register(NewUserDto userDto)
        {
            var registerResult = await _authRepository.Register(userDto.Email, userDto.Password, userDto.FirstName, userDto.LastName);
            if (registerResult.IsSuccess)
            {
                _logger.LogInformation("New user '{Email}' was created", userDto.Email);
                return null;

            }
            Log.Error("Error during adding new user '{Email}'", userDto.Email);

            return registerResult.Error;
        }
    }
}
