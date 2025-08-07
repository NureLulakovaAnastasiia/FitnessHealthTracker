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
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseService> _logger;


        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper, ILogger<ExerciseService> logger)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public Result<bool> AddExercise(Exercise exercise)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _exerciseRepository.AddExercise(exercise);
                _logger.LogInformation("Exercise '{Name}' was added", exercise.Name);

            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.Error(ex, "Error during adding exercise");

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
                _logger.LogInformation("User exercise for user '{UserId}' was added", exercise.UserId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.ForContext("Id", exercise.UserId)
                    .Error(ex, "Error during adding user exercise");

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
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                    .Error(ex, "Error during getting user exercises");

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
                res.Error = Errors.NoDataFoundMessage;
                Log.Error(ex, "Error during getting exercises");

            }
            return res;

        }

        public async Task<Result<bool>> RemoveExercise(int exerciseId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _exerciseRepository.DeleteExercise(exerciseId);
                _logger.LogInformation("Exercise '{exerciseId}' was deleted", exerciseId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.ForContext("Id", exerciseId)
                    .Error(ex, "Error during deleting exercise");

            }
            return res;
        }

        public async Task<Result<bool>> RemoveUserExercise(int exerciseId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _exerciseRepository.DeleteUserExercise(exerciseId);
                _logger.LogInformation("User exercise '{exerciseId}' was deleted", exerciseId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.ForContext("Id", exerciseId)
                    .Error(ex, "Error during deleting user exercise");

            }
            return res;
        }

        public Result<bool> UpdateExercise(Exercise exercise)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _exerciseRepository.UpdateExercise(exercise);
                _logger.LogInformation("Exercise '{Id}' was updated", exercise.Id);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", exercise.Id)
                    .Error(ex, "Error during updating exercise");

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
                _logger.LogInformation("User exercise '{Id}' was updated", exercise.Id);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", exercise.Id)
                    .Error(ex, "Error during updating user exercise");

            }
            return res;
        }
    }
}
