using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class MealService : IMealService
    {
        public bool AddMealHistory(MealHistory history)
        {
            throw new NotImplementedException();
        }

        public bool AddNewMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMeal(int mealId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMealHistory(int historyId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Meal> GetMeals()
        {
            throw new NotImplementedException();
        }

        public ICollection<MealHistory> GetMealsHistory(string userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMealHistory(MealHistory history)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMealNutrients(int mealId, MealNutrients nutrients)
        {
            throw new NotImplementedException();
        }
    }
}
