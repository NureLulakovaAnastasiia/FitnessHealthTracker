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
    public class UserStatisticsRepository : IUserStatisticsRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserStatisticsRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<MealHistory>> GetMealHistoryByDate(DateTime startDate, DateTime? endDate, string userId)
        {
            if (!endDate.HasValue)
            {
                endDate = DateTime.MaxValue;
            }

            return await _dbContext.MealHistories
                .Where(h => h.UserId == userId && (h.Date >= startDate && h.Date <= endDate))
                .Include(h => h.Meal)
                .ThenInclude(m => m.Nutrients)
                .ToListAsync();
        }

        public async Task<ICollection<UserExercise>> GetUserExercisesByDate(DateTime startDate, DateTime? endDate, string userId)
        {
            if (!endDate.HasValue)
            {
                endDate = DateTime.MaxValue;
            }

            return await _dbContext.UserExercises
                .Where(e => e.UserId == userId && (e.StartDate >= startDate && e.EndDate <= endDate))
                .Include(e => e.Exercise)
                .ToListAsync();

        }
    }
}
