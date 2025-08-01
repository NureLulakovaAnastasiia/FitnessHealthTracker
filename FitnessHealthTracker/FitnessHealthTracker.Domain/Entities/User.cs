using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserParameters Parameters { get; set; }
        public ICollection<UserExercise>? Exercises { get; set; }
        public ICollection<UserAim>? Aims { get; set; }
        public ICollection<Meal>? OwnMeals { get; set; }
        public ICollection<MealHistory>? Meals { get; set; }
        public ICollection<HealthParameter>? Weights { get; set; }

        public User()
        {
        }

        public User(string FirstName, string LastName, string Email, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            Parameters = new UserParameters();
        }

    }
}
