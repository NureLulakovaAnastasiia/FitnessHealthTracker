using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IExerciseService
    {
        public Task<Result<ICollection<Exercise>>> GetAvailableExercises();
        public Result<bool> AddExercise(Exercise exercise);
        public Task<Result<bool>> RemoveExercise(int exerciseId);
        public Result<bool> UpdateExercise(Exercise exercise);
        public Result<bool> AddUserExercise(UserExerciseDto exercise);
        public Result<bool> UpdateUserExercise(UserExerciseDto exercise);
        public Task<Result<bool>> RemoveUserExercise(int exerciseId);
        public Task<Result<ICollection<UserExercise>>> GetAllUserExercises(string userId);


    }
}
