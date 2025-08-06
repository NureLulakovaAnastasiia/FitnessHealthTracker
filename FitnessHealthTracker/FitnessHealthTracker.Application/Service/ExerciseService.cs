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
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        public Result<bool> AddExercise(Exercise exercise)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _exerciseRepository.AddExercise(exercise);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public Result<bool> AddUserExercise(UserExerciseDto exercise)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var aimToAdd = _mapper.Map<UserExercise>(exercise);
                res.Value = _exerciseRepository.AddUserExercise(aimToAdd, exercise.UserId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;

        }

        public async Task<Result<ICollection<UserExercise>>> GetAllUserExercises(string userId)
        {
            var res = new Result<ICollection<UserExercise>>();
            try
            {
                res.Value = await _exerciseRepository.GetAllUserExercises(userId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<ICollection<Exercise>>> GetAvailableExercises()
        {
            var res = new Result<ICollection<Exercise>>();
            try
            {
                res.Value = await _exerciseRepository.GetAllExercises();
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;

        }

        public async Task<Result<bool>> RemoveExercise(int exerciseId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _exerciseRepository.DeleteExercise(exerciseId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<bool>> RemoveUserExercise(int exerciseId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _exerciseRepository.DeleteUserExercise(exerciseId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public Result<bool> UpdateExercise(Exercise exercise)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _exerciseRepository.UpdateExercise(exercise);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public Result<bool> UpdateUserExercise(UserExerciseDto exercise)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var exerciseToUpdate = _mapper.Map<UserExercise>(exercise);
                res.Value = _exerciseRepository.UpdateUserExercise(exerciseToUpdate);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }
    }
}
