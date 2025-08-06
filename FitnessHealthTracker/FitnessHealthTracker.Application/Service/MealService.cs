using AutoMapper;
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

    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;


        public MealService(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public Result<bool> AddMealHistory(MealHistoryDto history)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var historyToAdd = _mapper.Map<MealHistory>(history);
                res.Value = _mealRepository.AddMealHistory(historyToAdd, history.UserId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public Result<bool> AddNewMeal(Meal meal)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _mealRepository.AddMeal(meal);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<bool>> DeleteMeal(int mealId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _mealRepository.DeleteMeal(mealId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;

        }

        public async Task<Result<bool>> DeleteMealHistory(int historyId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _mealRepository.DeleteMealHistory(historyId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
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
                res.Error = ex.Message;
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
                res.Error = ex.Message;
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
                res.Error = ex.Message;
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
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
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
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public Result<bool> UpdateMealNutrients(int mealId, MealNutrients nutrients)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _mealRepository.UpdateMealNutrients(nutrients, mealId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }
    }
}
