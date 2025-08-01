using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
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

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public void ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAccount(string userId)
        {
            throw new NotImplementedException();
        }

        public void LogIn(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public void Register(NewUserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
