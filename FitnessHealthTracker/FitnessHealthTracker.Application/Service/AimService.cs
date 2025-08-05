using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
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

        public AimService(IAimRepository aimRepository, IMapper mapper)
        {
            _aimRepository = aimRepository;
            _mapper = mapper;
        }
        public Result<bool> AddAim(Aim aim)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _aimRepository.AddAim(aim);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
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
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<bool>> DeleteAim(int aimId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _aimRepository.DeleteAim(aimId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public async Task<Result<bool>> DeleteUserAim(int userAimId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _aimRepository.DeleteUserAim(userAimId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
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
                res.Error = ex.Message;
            }
            return res;

        }

        public async Task<Result<ICollection<UserAim>>> GetAllUserAims(string userId)
        {
            var res = new Result<ICollection<UserAim>>();
            try
            {
                res.Value = await _aimRepository.GetAllUserAims(userId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
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
                res.Error = ex.Message;
            }
            return res;

        }

        public async Task<Result<bool>> MarkUserAimAchieved(int userAimId)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = await _aimRepository.MarkUserAimAchieved(userAimId);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }

        public Result<bool> UpdateAim(Aim aim)
        {
            var res = new Result<bool>() { Value = false };
            try
            {
                res.Value = _aimRepository.UpdateAim(aim);
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
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
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
            }
            return res;
        }
    }
}
