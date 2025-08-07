using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IAuthService
    {
        public Task<string?> Register(NewUserDto userDto);
        public Task<Result<string>> LogIn(UserLoginDto userLoginDto);

    }
}
