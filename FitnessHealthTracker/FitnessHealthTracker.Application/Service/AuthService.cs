using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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

        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        public Task<Result<bool>> ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAccount(string userId)
        {
            throw new NotImplementedException();
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
                    return result;
                }

                var user = await _userRepository.GetUserByEmail(userLoginDto.Email);
                if (user == null)
                {
                    result.Error = Errors.LoginErrorMessage;
                }
                else
                {
                    var token = _tokenService.GenerateToken(user);
                    if (token == null)
                    {
                        result.Error = Errors.LoginErrorMessage;
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
            }

            return result;
        }

        public async Task<string?> Register(NewUserDto userDto)
        {
            var registerResult = await _authRepository.Register(userDto.Email, userDto.Password, userDto.FirstName, userDto.LastName);
            if (registerResult.IsSuccess)
            {
                return null;
            }
            return registerResult.Error;
        }
    }
}
