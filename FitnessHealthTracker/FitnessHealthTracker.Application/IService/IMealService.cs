using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Domain;
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
        public Task<Result<ICollection<Meal>>> GetMeals();
        public Result<bool> AddNewMeal(Meal meal);
        public Result<bool> UpdateMeal(UpdateMealDto mealDto);
        public Result<bool> UpdateMealNutrients(int mealId, MealNutrients nutrients);
        public Task<Result<bool>> DeleteMeal(int mealId);
        public Task<Result<ICollection<MealHistory>>> GetMealsHistory(string userId);
        public Result<bool> AddMealHistory(MealHistoryDto history);
        public Result<bool> UpdateMealHistory(MealHistoryDto history);
        public Task<Result<bool>> DeleteMealHistory(int historyId);
        public Task<Result<ICollection<Meal>>> GetUserMeals(string userId); //which user added


    }
}
