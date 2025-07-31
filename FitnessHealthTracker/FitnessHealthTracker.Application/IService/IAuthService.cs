using FitnessHealthTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IAuthService
    {
        public void Register(NewUserDto userDto);
        public void LogIn(UserLoginDto userLoginDto);
        public void ChangePassword(string oldPassword, string newPassword);
        public bool DeleteAccount(string userId);

    }
}
