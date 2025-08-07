using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class AimService : IAimService
    {
        private readonly IAimRepository _aimRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AimService> _logger;

        public AimService(IAimRepository aimRepository, IMapper mapper, ILogger<AimService> logger)
        {
            _aimRepository = aimRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public Result<bool> AddAim(Aim aim)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _aimRepository.AddAim(aim);
                _logger.LogInformation("Aim with name '{Name}' was created", aim.Name);
            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.Error(ex, "Error during adding aim");

            }
            return res;
        }

        public Result<bool> AddUserAim(UserAimDto userAim)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var aimToAdd = _mapper.Map<UserAim>(userAim);
                res.Value = _aimRepository.AddUserAim(aimToAdd, userAim.UserId);
                _logger.LogInformation("Aim for user '{UserId}' was created", userAim.UserId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.AddingErrorMessage;
                Log.ForContext("UserId", userAim.UserId)
                    .Error(ex, "Error during adding user aim");

            }
            return res;
        }

        public async Task<Result<bool>> DeleteAim(int aimId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _aimRepository.DeleteAim(aimId);
                _logger.LogInformation("Aim with id '{aimId}' was deleted", aimId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.Error(ex, "Error during deleting aim");

            }
            return res;
        }

        public async Task<Result<bool>> DeleteUserAim(int userAimId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _aimRepository.DeleteUserAim(userAimId);
                _logger.LogInformation("User aim '{userAimId}' was deleted", userAimId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.DeletingErrorMessage;
                Log.Error(ex, "Error during deleting aim");
            }
            return res;
        }

        public async Task<Result<ICollection<Aim>>> GetAllAims()
        {
            var res = new Result<ICollection<Aim>>();
            try
            {
                res.Value = await _aimRepository.GetAllAims();
            }
            catch (Exception ex)
            {
                res.Error = Errors.NoDataFoundMessage;
            }
            return res;

        }

        public async Task<Result<ICollection<UserAim>>> GetAllUserAims(string userId)
        {
            var res = new Result<ICollection<UserAim>>();
            try
            {
                res.Value = await _aimRepository.GetAllUserAims(userId);
                _logger.LogInformation("Aims for user '{userId}' were gotten", userId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                    .Error(ex, "Error during getting user aims");

            }
            return res;
        }

        public async Task<Result<UserAim>> GetLatestUserAim(string userId)
        {
            var res = new Result<UserAim>();
            try
            {
                var allAims = await _aimRepository.GetAllUserAims(userId);
                if (allAims.Any())
                {
                    res.Value = allAims.MaxBy(x => x.StartDate);

                }
            }
            catch (Exception ex)
            {
                res.Error = Errors.NoDataFoundMessage;
                Log.ForContext("UserId", userId)
                    .Error(ex, "Error during getting latest user aim");

            }
            return res;

        }

        public async Task<Result<bool>> MarkUserAimAchieved(int userAimId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _aimRepository.MarkUserAimAchieved(userAimId);
                _logger.LogInformation("Aim '{userAimId}' status was changed to achieved", userAimId);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("UserAimId", userAimId)
                    .Error(ex, "Error during updating user aim status to achieved");

            }
            return res;
        }

        public Result<bool> UpdateAim(Aim aim)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _aimRepository.UpdateAim(aim);
                _logger.LogInformation("Aim '{Id}' was updated", aim.Id);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", aim.Id)
                    .Error(ex, "Error during updating aim");

            }
            return res;
        }

        public Result<bool> UpdateUserAim(UserAimDto userAim)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                var aimToUpdate = _mapper.Map<UserAim>(userAim);
                res.Value = _aimRepository.UpdateUserAim(aimToUpdate);
                _logger.LogInformation("User aim '{Id}' was updated", userAim.Id);

            }
            catch (Exception ex)
            {
                res.Error = Errors.UpdatingErrorMessage;
                Log.ForContext("Id", userAim.Id)
                    .Error(ex, "Error during updating aim");

            }
            return res;
        }
    }
}
