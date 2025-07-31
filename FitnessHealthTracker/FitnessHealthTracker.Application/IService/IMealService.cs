using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IMealService
    {
        public ICollection<Meal> GetMeals();
        public bool AddNewMeal(Meal meal);
        public bool UpdateMeal(Meal meal);
        public bool UpdateMealNutrients(int mealId, MealNutrients nutrients);
        public bool DeleteMeal(int mealId);
        public ICollection<MealHistory> GetMealsHistory(string userId);
        public bool AddMealHistory(MealHistory history);
        public bool UpdateMealHistory(MealHistory history);
        public bool DeleteMealHistory(int historyId);

    }
}
