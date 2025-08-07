using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{

    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MealService> _logger;



        public MealService(IMealRepository mealRepository, IMapper mapper, ILogger<MealService> logger)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Result<bool> AddMealHistory(MealHistoryDto history)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var historyToAdd = _mapper.Map<MealHistory>(history);
                res.Value = _mealRepository.AddMealHistory(historyToAdd, history.UserId);
                _logger.LogInformation("Meal history for user '{UserId}' was added", history.UserId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.ForContext("UserId", history.UserId)
                    .Error(ex, "Error during adding meal history");

            }
            return res;
        }

        public Result<bool> AddNewMeal(Meal meal)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _mealRepository.AddMeal(meal);
                _logger.LogInformation("Meal '{Name}' was added", meal.Name);

            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.Error(ex, "Error during adding meal");

            }
            return res;
        }

        public async Task<Result<bool>> DeleteMeal(int mealId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _mealRepository.DeleteMeal(mealId);
                _logger.LogInformation("Meal '{mealId}' was deleted", mealId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.ForContext("Id", mealId)
                    .Error(ex, "Error during deleting meal");

            }
            return res;

        }

        public async Task<Result<bool>> DeleteMealHistory(int historyId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _mealRepository.DeleteMealHistory(historyId);
                _logger.LogInformation("Meal history '{historyId}' was deleted", historyId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.ForContext("Id", historyId)
                    .Error(ex, "Error during deleting meal history");

            }
            return res;
        }

        public async Task<Result<ICollection<Meal>>> GetMeals()
        {
            var res = new Result<ICollection<Meal>>();
            try
            {
                res.Value = await _mealRepository.GetAllMeals();
            }
            catch (Exception ex)
            {
                res.Error = Errors.NoDataFoundMessage;
                Log.Error(ex, "Error during getting meals");

            }
            return res;
        }

        public async Task<Result<ICollection<MealHistory>>> GetMealsHistory(string userId)
        {
            var res = new Result<ICollection<MealHistory>>();
            try
            {
                res.Value = await _mealRepository.GetAllUserMealHistory(userId);
            }
            catch (Exception ex)
            {
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                    .Error(ex, "Error during getting meals history");

            }
            return res;
        }

        public async Task<Result<ICollection<Meal>>> GetUserMeals(string userId)
        {
            var res = new Result<ICollection<Meal>>();
            try
            {
                var allMeals = await _mealRepository.GetAllMeals();
                if (allMeals != null)
                {
                    res.Value = allMeals.Where(m => m.UserId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                .Error(ex, "Error during getting user meals");

            }
            return res;

        }

        public Result<bool> UpdateMeal(UpdateMealDto mealDto)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var mealToUpdate = _mapper.Map<Meal>(mealDto);
                res.Value = _mealRepository.UpdateMeal(mealToUpdate);
                _logger.LogInformation("Meal '{Id}' was updated", mealDto.Id);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", mealDto.Id)
                    .Error(ex, "Error during updating meal");

            }
            return res;
        }

        public Result<bool> UpdateMealHistory(MealHistoryDto history)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var historyToUpdate = _mapper.Map<MealHistory>(history);
                res.Value = _mealRepository.UpdateMealHistory(historyToUpdate, history.UserId);
                _logger.LogInformation("Meal history '{Id}' was updated", history.Id);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", history.Id)
                    .Error(ex, "Error during updating meal history");

            }
            return res;
        }

        public Result<bool> UpdateMealNutrients(int mealId, MealNutrients nutrients)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _mealRepository.UpdateMealNutrients(nutrients, mealId);
                _logger.LogInformation("Meal nutrients '{Id}' for meal '{mealId}' were updated", nutrients.Id, mealId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", nutrients.Id)
                    .Error(ex, "Error during updating meal nutrients");

            }
            return res;
        }
    }
}
