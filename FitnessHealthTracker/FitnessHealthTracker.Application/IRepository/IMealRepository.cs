using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IMealRepository
    {
        public bool AddMeal(Meal meal);
        public bool UpdateMeal(Meal meal);
        public Task<bool> DeleteMeal(int mealId);
        public Task<ICollection<Meal>?> GetAllMeals();
        public bool AddMealHistory(MealHistory history, string userId);
        public bool UpdateMealHistory(MealHistory meal, string userId);
        public bool UpdateMealNutrients(MealNutrients nutrients, int mealId);
        public Task<bool> DeleteMealHistory(int mealHistId);
        public Task<ICollection<MealHistory>?> GetAllUserMealHistory(string userId);

    }
}
