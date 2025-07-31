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
        public ICollection<Exercise> GetAvailableExercises();
        public bool AddExercise(Exercise exercise);
        public bool RemoveExercise(int exerciseId);
        public bool UpdateExercise(Exercise exercise);
        public bool AddUserExercise(UserExercise exercise);
        public bool UpdateUserExercise(UserExercise exercise);
        public bool RemoveUserExercise(int exerciseId);

    }
}
