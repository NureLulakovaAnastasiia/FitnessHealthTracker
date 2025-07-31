using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserParameters Parameters { get; set; }
        public ICollection<UserExercise>? Exercises { get; set; }
        public ICollection<UserAim>? Aims { get; set; }
        public ICollection<Meal>? OwnMeals { get; set; }
        public ICollection<MealHistory>? Meals { get; set; }
        public ICollection<WeightParameter> Weights { get; set; }

        public User()
        {
        }

        public User(string FirstName, string LastName, string Email, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            Parameters = new UserParameters();
        }

    }
}
