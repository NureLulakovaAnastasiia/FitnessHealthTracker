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
        public ICollection<Aim> GetAllAim();
        public bool AddAim(Aim aim);
        public bool DeleteAim(int aimId);
        public bool UpdateAim(Aim aim);
        public bool AddUserAim(UserAim userAim);
        public bool DeleteUserAim(int userAimId);
        public bool UpdateUserAim(UserAim userAim);
        public ICollection<UserAim> GetAllUserAims(string userId);
        public bool MarkUserAimAchieved(int userAimId);
        public UserAim GetLatestUserAim(int userId);
    }
}
