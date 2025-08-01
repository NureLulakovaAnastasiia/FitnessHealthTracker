using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
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

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }
        public bool AddExercise(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public bool AddUserExercise(UserExercise exercise)
        {
            throw new NotImplementedException();
        }

        public ICollection<Exercise> GetAvailableExercises()
        {
            throw new NotImplementedException();
        }

        public bool RemoveExercise(int exerciseId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserExercise(int exerciseId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateExercise(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserExercise(UserExercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
