using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IHealthParametersService
    {
        public Result<bool> AddParameter(HealthParameter parameter);
        public Task<Result<bool>> RemoveParameter(int parameterId);
        public Task<Result<ICollection<HealthParameter>>> GetParametersByType(HealthParameterType type, string userId);
        public Task<Result<ICollection<HealthParameter>?>> GetParametersInTimeInterval(DateTime? start, DateTime? end, string userId);

    }
}

