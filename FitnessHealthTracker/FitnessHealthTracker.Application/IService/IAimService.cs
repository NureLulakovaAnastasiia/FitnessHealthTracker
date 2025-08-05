using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface IAimService
    {
        public Task<Result<ICollection<Aim>>> GetAllAims();
        public Result<bool> AddAim(Aim aim);
        public Task<Result<bool>> DeleteAim(int aimId);
        public Result<bool> UpdateAim(Aim aim);
        public Result<bool> AddUserAim(UserAimDto userAim);
        public Task<Result<bool>> DeleteUserAim(int userAimId);
        public Result<bool> UpdateUserAim(UserAimDto userAim);
        public Task<Result<ICollection<UserAim>>> GetAllUserAims(string userId);
        public Task<Result<bool>> MarkUserAimAchieved(int userAimId);
        public Task<Result<UserAim>> GetLatestUserAim(string userId);
    }
}
