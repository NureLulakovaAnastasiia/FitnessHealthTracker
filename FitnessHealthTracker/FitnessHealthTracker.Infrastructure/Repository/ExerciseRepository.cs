using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using FitnessHealthTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Infrastructure.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ExerciseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddExercise(Exercise exercise)
        {
            _dbContext.Add(exercise);
            return _dbContext.SaveChanges() != 0;
        }

        public bool AddUserExercise(UserExercise exercise, string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.Exercises = new List<UserExercise>() { exercise };
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteExercise(int exerciseId)
        {
            var exerciseToRemove = await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == exerciseId);
            if (exerciseToRemove != null)
            {
                _dbContext.Exercises.Remove(exerciseToRemove);
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteUserExercise(int exerciseId)
        {
            var exerciseToRemove = await _dbContext.UserExercises.FirstOrDefaultAsync(e => e.Id == exerciseId);
            if (exerciseToRemove != null)
            {
                _dbContext.UserExercises.Remove(exerciseToRemove);
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<ICollection<Exercise>?> GetAllExercises()
        {
            return await _dbContext.Exercises.ToListAsync();
        }

        public async Task<ICollection<UserExercise>?> GetAllUserExercises(string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Exercises)
                .FirstOrDefaultAsync();
            if (user != null && user.Exercises != null)
            {
                return user.Exercises;
            }
            return null;
        }

        public bool UpdateExercise(Exercise exercise)
        {
            _dbContext.Update(exercise);
            return _dbContext.SaveChanges() != 0;
        }

        public bool UpdateUserExercise(UserExercise exercise)
        {
            _dbContext.Update(exercise);
            return _dbContext.SaveChanges() != 0;
        }
    }
}
