using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<Meal> GetUserMeals()
        {
            throw new NotImplementedException();
        }

        public UserParameters GetUserParameters(string userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserParameters(UserParameters userParameters)
        {
            throw new NotImplementedException();
        }
    }
}
