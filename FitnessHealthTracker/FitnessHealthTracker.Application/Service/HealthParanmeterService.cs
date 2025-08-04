using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class HealthParanmeterService : IHealthParametersService
    {
        public bool AddParameter(HealthParameter parameter)
        {
            throw new NotImplementedException();
        }

        public ICollection<HealthParameter> GetParametersByType(HealthParameterType type, string userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveParameter(HealthParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}
