using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class HealthParameterService : IHealthParametersService
    {
        private readonly IHealthParametersRepository _healthParametersRepository;
        private readonly ILogger<HealthParameterService> _logger;


        public HealthParameterService(IHealthParametersRepository healthParametersRepository, ILogger<HealthParameterService> logger)
        {
            _healthParametersRepository = healthParametersRepository;
            _logger = logger;
        }
        public Result<bool> AddParameter(HealthParameter parameter)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _healthParametersRepository.AddParameter(parameter);
                _logger.LogInformation("Health parameter for user '{UserId}' was added", parameter.UserId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.ForContext("UserId", parameter.UserId)
                .Error(ex, "Error during adding health parameter");

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
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                    .ForContext("Type", type.ToString())
                    .Error(ex, "Error during getting health parameters");

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
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                    .Error(ex, "Error during getting health parameters");

            }
            return res;
        }

        public async Task<Result<bool>> RemoveParameter(int parameterId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _healthParametersRepository.RemoveParameter(parameterId);
                _logger.LogInformation("Health Parameter '{parameterId}' was deleted", parameterId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.ForContext("Id", parameterId)
                    .Error(ex, "Error during deleting health parameter");

            }
            return res;

        }
    }
}
