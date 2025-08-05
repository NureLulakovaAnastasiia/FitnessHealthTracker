using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IAimRepository
    {
        public bool AddAim(Aim aim);
        public bool UpdateAim(Aim aim);
        public Task<bool> DeleteAim(int aimId);
        public bool AddUserAim(UserAim userAim, string userId);
        public bool UpdateUserAim(UserAim userAim);
        public Task<bool> DeleteUserAim(int aimId);
        public Task<ICollection<Aim>> GetAllAims();
        public Task<ICollection<UserAim>?> GetAllUserAims(string userId);
        public Task<bool> MarkUserAimAchieved(int userAimId);
    }
}
