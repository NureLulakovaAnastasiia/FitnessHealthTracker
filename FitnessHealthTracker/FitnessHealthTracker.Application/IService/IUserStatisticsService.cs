using FitnessHealthTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IUserStatisticsService
    {
        public CaloriesPerDayDto GetTotalCaloriesPerDate(DateTime startDate, DateTime? endDate);
        public CaloriesPerMealDto GetTotalCaloriesPerMealDate(DateTime startDate, DateTime? endDate);
        public CaloriesPerMealDto GetTotalCaloriesPerActivityDate(DateTime startDate, DateTime? endDate);

    }
}
