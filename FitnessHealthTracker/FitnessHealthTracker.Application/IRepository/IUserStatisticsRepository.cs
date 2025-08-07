using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IUserStatisticsRepository
    {
        public Task<ICollection<MealHistory>> GetMealHistoryByDate(DateTime startDate, DateTime? endDate, string userId);
        public Task<ICollection<UserExercise>> GetUserExercisesByDate(DateTime startDate, DateTime? endDate, string userId);

    }
}
