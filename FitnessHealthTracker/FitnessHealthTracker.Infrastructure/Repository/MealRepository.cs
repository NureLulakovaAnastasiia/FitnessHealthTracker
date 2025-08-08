using AutoMapper;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Domain.Entities;
using FitnessHealthTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Infrastructure.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public MealRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddMeal(Meal meal)
        {
            _dbContext.Add(meal);
            return _dbContext.SaveChanges() != 0;
        }

        public bool AddMealHistory(MealHistory history, string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.Meals = new List<MealHistory>() { history };
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteMeal(int mealId)
        {
            var mealToRemove = await _dbContext.Meals
                .Where(e => e.Id == mealId)
                .Include(e => e.Nutrients)
                .FirstOrDefaultAsync();
            if (mealToRemove != null)
            {
                _dbContext.Remove(mealToRemove.Nutrients);
                _dbContext.Remove(mealToRemove);
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteMealHistory(int mealHistId)
        {
            var historyToRemove = await _dbContext.MealHistories.FirstOrDefaultAsync(e => e.Id == mealHistId);
            if (historyToRemove != null)
            {
                _dbContext.MealHistories.Remove(historyToRemove);
            }
            return _dbContext.SaveChanges() != 0;

        }

        public async Task<ICollection<Meal>?> GetAllMeals()
        {
            return await _dbContext.Meals
                .Include(m => m.Nutrients)
                .ToListAsync();
        }

        public async Task<ICollection<MealHistory>?> GetAllUserMealHistory(string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Meals)
                .FirstOrDefaultAsync();
            if (user != null && user.Meals != null)
            {
                return user.Meals;
            }
            return null;
        }

        public bool UpdateMeal(Meal meal)
        {
            var mealToUpdate = _dbContext.Meals.FirstOrDefault(m => m.Id == meal.Id);
            if (mealToUpdate != null)
            {
                mealToUpdate.Name = meal.Name;
            }
            return _dbContext.SaveChanges() != 0;
        }

        public bool UpdateMealHistory(MealHistory history, string userId)
        {
            _dbContext.Update(history);
            return _dbContext.SaveChanges() != 0;
        }

        public bool UpdateMealNutrients(MealNutrients nutrients, int mealId)
        {
            var meal = _dbContext.Meals.FirstOrDefault(m => m.Id == mealId);
            if (meal != null)
            {
                meal.Nutrients = nutrients;
            }
            return _dbContext.SaveChanges() != 0;
        }
    }
}
