using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly IUserStatisticsRepository _userStatisticsRepository;

        public UserStatisticsService(IUserStatisticsRepository userStatisticsRepository)
        {
            _userStatisticsRepository = userStatisticsRepository;
        }

        public async Task<Result<ICollection<CaloriesPerActivityDto>>> GetTotalCaloriesPerActivityDate(DateTime startDate, DateTime? endDate, string userId)
        {
            var result = new Result<ICollection<CaloriesPerActivityDto>>();
            try
            {
                var userExercises = await _userStatisticsRepository.GetUserExercisesByDate(startDate, endDate, userId);
                if (userExercises != null && userExercises.Any())
                {
                    var exercisesCalories = new List<CaloriesPerActivityDto>();
                    foreach (var exercise in userExercises)
                    {
                        var countedActivity = CountExerciseCalories(exercise);
                        if (countedActivity != null)
                        {
                            exercisesCalories.Add(countedActivity);
                        }
                    }
                    exercisesCalories.OrderBy(m => m.Date);
                    result.Value = exercisesCalories;
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public async Task<Result<ICollection<CaloriesPerDayDto>>> GetTotalCaloriesPerDate(DateTime startDate, DateTime? endDate, string userId)
        {
            var result = new Result<ICollection<CaloriesPerDayDto>>();
            try
            {
                var countedMeals = await GetTotalCaloriesPerMealDate(startDate, endDate, userId);
                if (!countedMeals.IsSuccess)
                {
                    result.Error = countedMeals.Error;
                    return result;
                }

                var countedActivity = await GetTotalCaloriesPerActivityDate(startDate, endDate, userId);
                if (!countedActivity.IsSuccess)
                {
                    result.Error = countedActivity.Error;
                    return result;
                }

                if(endDate == null)
                {
                    endDate = startDate;
                }

                var listIfDayCalories = new List<CaloriesPerDayDto>();
                if (countedActivity.Value != null && countedMeals.Value != null)
                {
                    var dates = countedMeals.Value.Select(m => m.Date.Date)
                        .Concat(countedActivity.Value.Select(a => a.Date.Date)).Distinct().ToList();

                   foreach (var date in dates) {
                        var dayCalories = new CaloriesPerDayDto();
                        dayCalories.Date = date;
                        dayCalories.Activities = countedActivity.Value.Where(a => a.Date.Date == date).ToList();
                        dayCalories.Meals = countedMeals.Value.Where(a => a.Date.Date == date).ToList();
                        listIfDayCalories.Add(dayCalories);
                        startDate.AddDays(1);
                    }
                }
                result.Value = listIfDayCalories;
            }
            catch (Exception ex)
            {
                result.Error += ex.Message;
            }

            return result;
        }


        //return list of meal portions eaten by user in given time period with counted calories and weight of portion in grams
        public async Task<Result<ICollection<CaloriesPerMealDto>>> GetTotalCaloriesPerMealDate(DateTime startDate, DateTime? endDate, string userId)
        {
            var result =new Result<ICollection<CaloriesPerMealDto>>();
            try
            {
                var mealHistories = await _userStatisticsRepository.GetMealHistoryByDate(startDate, endDate, userId);
                if (mealHistories != null && mealHistories.Any())
                {
                    var mealCalories = new List<CaloriesPerMealDto>();
                    foreach (var meal in mealHistories)
                    {
                        var countedMeal = CountMealCalories(meal);
                        if (countedMeal != null)
                        {
                            mealCalories.Add(countedMeal);
                        }
                    }
                    mealCalories.OrderBy(m => m.Date);
                    result.Value = mealCalories;
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        private CaloriesPerMealDto? CountMealCalories(MealHistory history)
        {
            if (history.Meal != null && history.Meal.Nutrients != null)
            {
                return new CaloriesPerMealDto
                {
                    Id = history.Id,
                    Name = history.Meal.Name,
                    CaloriesValue = history.Meal.Nutrients.CaloriesPerHundredGrams * history.WeightInGrams / 100,
                    MealWeight = history.WeightInGrams,
                    Date = history.Date,
                };
             }
            return null;
        }

        private CaloriesPerActivityDto? CountExerciseCalories(UserExercise exercise)
        {
            if (exercise.Exercise != null)
            {
                return new CaloriesPerActivityDto
                {
                    Id = exercise.Id,
                    Name = exercise.Exercise.Name,
                    CaloriesValue = exercise.Calories,
                    TimeInMinutes = (exercise.EndDate - exercise.StartDate).TotalMinutes,
                    Date = exercise.StartDate
                };
            }
            return null;
        }

    }
}
