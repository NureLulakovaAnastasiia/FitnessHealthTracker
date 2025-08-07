using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IUserStatisticsService
    {
        public Task<Result<ICollection<CaloriesPerDayDto>>> GetTotalCaloriesPerDate(DateTime startDate, DateTime? endDate, string userId);
        public Task<Result<ICollection<CaloriesPerMealDto>>> GetTotalCaloriesPerMealDate(DateTime startDate, DateTime? endDate, string userId);
        public Task<Result<ICollection<CaloriesPerActivityDto>>> GetTotalCaloriesPerActivityDate(DateTime startDate, DateTime? endDate, string userId);

    }
}
