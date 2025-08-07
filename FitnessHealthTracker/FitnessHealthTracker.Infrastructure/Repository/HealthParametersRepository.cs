using FitnessHealthTracker.Application.IRepository;
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
    public class HealthParametersRepository : IHealthParametersRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public HealthParametersRepository(ApplicationDBContext dBContext) 
        { 
            _dbContext = dBContext;
        }
        public bool AddParameter(HealthParameter parameter)
        {
            _dbContext.Add(parameter);
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<ICollection<HealthParameter>?> GetParametersByType(HealthParameterType type, string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.HealthParameters)
                .FirstOrDefaultAsync();
            if (user != null && user.HealthParameters != null)
            {
                return user.HealthParameters.Where(p => p.Type == type).ToList();
            }

            return null;
        }

        public async Task<ICollection<HealthParameter>?> GetParametersInTimeInterval(DateTime? start, DateTime? end, string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.HealthParameters)
                .FirstOrDefaultAsync();
            if (user != null && user.HealthParameters != null)
            {
                if (!start.HasValue)
                {
                    start = DateTime.MinValue;
                }
                if (!end.HasValue)
                {
                    end = DateTime.MaxValue;
                }

                return user.HealthParameters
                    .Where(p => p.DateTime >= start && p.DateTime <= end).ToList();
            }

            return null;

        }

        public async Task<bool> RemoveParameter(int parameterId)
        {
            var parameter = await _dbContext.HealthParameters.FirstOrDefaultAsync(p => p.Id == parameterId);
            if (parameter != null)
            {
                _dbContext.HealthParameters.Remove(parameter);
            }
            return _dbContext.SaveChanges() != 0;
        }
    }
}
