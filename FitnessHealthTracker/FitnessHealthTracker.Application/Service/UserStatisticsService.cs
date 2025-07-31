using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    internal class UserStatisticsService : IUserStatisticsService
    {
        public CaloriesPerMealDto GetTotalCaloriesPerActivityDate(DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public CaloriesPerDayDto GetTotalCaloriesPerDate(DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public CaloriesPerMealDto GetTotalCaloriesPerMealDate(DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }
    }
}
