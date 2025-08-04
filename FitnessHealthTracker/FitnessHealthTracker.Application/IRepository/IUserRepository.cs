using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IUserRepository
    {
        public Task<User?> GetUserById(string id);
        public Task<User?> GetUserByEmail(string userEmail);
        public Task<Result<UserParameters>> GetUserParameters(string userId);
        public Task<bool> UpdateUserParameters(UserParameters userParameters);

    }
}
