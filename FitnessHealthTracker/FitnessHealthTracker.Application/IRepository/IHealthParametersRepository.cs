using FitnessHealthTracker.Domain.Entities;
using FitnessHealthTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IHealthParametersRepository
    {
        public bool AddParameter(HealthParameter parameter);
        public Task<bool> RemoveParameter(int parameterId);
        public Task<ICollection<HealthParameter>?> GetParametersByType(HealthParameterType type, string userId);

        public Task<ICollection<HealthParameter>?> GetParametersInTimeInterval(DateTime? start, DateTime? end, string userId);
    }
}
