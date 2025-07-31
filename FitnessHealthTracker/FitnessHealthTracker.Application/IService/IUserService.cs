using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IUserService
    {
        public UserParameters GetUserParameters(string userId);
        public bool UpdateUserParameters(UserParameters userParameters);
        public ICollection<Meal> GetUserMeals();

    }
}
