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
