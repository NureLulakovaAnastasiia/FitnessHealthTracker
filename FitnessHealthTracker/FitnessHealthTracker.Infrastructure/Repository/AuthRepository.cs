using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager; 
        }

        public async Task<Result<bool>> LogIn(string email, string password)
        {
            var result = new Result<bool>();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                result.Error = Errors.UserDoesntExistsMessage;
                return result;
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
            if (passwordCheck)
            {
                result.Value = true;
            }
            else
            {
                result.Error = Errors.WrongPasswordMessage;
            }

            return result;
        }

        public async Task<Result<bool>> Register(string email, string password, string firstName, string lastName)
        {
            var result = new Result<bool>();
            var alreadyExists = await _userManager.FindByEmailAsync(email) != null;
            if (alreadyExists)
            {
                result.Error = Errors.UserAlreadyExistsMessage;
                return result;
            }
            var newUser = new User(firstName, lastName, email);
            var creationResult = await _userManager.CreateAsync(newUser, password);
            if (creationResult.Succeeded)
            {
                result.Value = true;
            }
            else
            {
                result.Error = creationResult.Errors.FirstOrDefault().Description;
            }
            return result;
        }
    }
}
