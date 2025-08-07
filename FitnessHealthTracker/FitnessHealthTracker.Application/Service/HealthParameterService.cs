using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class HealthParameterService : IHealthParametersService
    {
        private readonly IHealthParametersRepository _healthParametersRepository;

        public HealthParameterService(IHealthParametersRepository healthParametersRepository)
        {
            _healthParametersRepository = healthParametersRepository;
        }
        public Result<bool> AddParameter(HealthParameter parameter)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _healthParametersRepository.AddParameter(parameter);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;

        }

        public async Task<Result<ICollection<HealthParameter>>> GetParametersByType(HealthParameterType type, string userId)
        {
            var res = new Result<ICollection<HealthParameter>>();
            try
            {
                res.Value = await _healthParametersRepository.GetParametersByType(type, userId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<ICollection<HealthParameter>?>> GetParametersInTimeInterval(DateTime? start, DateTime? end, string userId)
        {
            var res = new Result<ICollection<HealthParameter>>();
            try
            {
                res.Value = await _healthParametersRepository.GetParametersInTimeInterval(start, end, userId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<bool>> RemoveParameter(int parameterId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _healthParametersRepository.RemoveParameter(parameterId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;

        }
    }
}
