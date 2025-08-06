using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IExerciseRepository
    {
        public bool AddExercise(Exercise exercise);
        public bool UpdateExercise(Exercise exercise);
        public Task<bool> DeleteExercise(int exerciseId);
        public Task<ICollection<Exercise>?> GetAllExercises();
        public bool AddUserExercise(UserExercise exercise, string userId);
        public bool UpdateUserExercise(UserExercise exercise);
        public Task<bool> DeleteUserExercise(int exerciseId);
        public Task<ICollection<UserExercise>?> GetAllUserExercises(string userId);

    }
}
