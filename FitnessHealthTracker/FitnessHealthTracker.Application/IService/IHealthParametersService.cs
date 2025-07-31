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
        public bool AddParameter(HealthParameter parameter);
        public bool RemoveParameter(HealthParameter parameter);
        public ICollection<HealthParameter> GetParametersByType(HealthParameterType type, string userId);
    }
}
