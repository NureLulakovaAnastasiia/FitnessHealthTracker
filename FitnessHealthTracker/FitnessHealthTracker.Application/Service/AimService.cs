using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class AimService : IAimService
    {
        public bool AddAim(Aim aim)
        {
            throw new NotImplementedException();
        }

        public bool AddUserAim(UserAim userAim)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAim(int aimId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserAim(int userAimId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Aim> GetAllAim()
        {
            throw new NotImplementedException();
        }

        public ICollection<UserAim> GetAllUserAims(string userId)
        {
            throw new NotImplementedException();
        }

        public bool MarkUserAimAchieved(int userAimId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAim(Aim aim)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserAim(UserAim userAim)
        {
            throw new NotImplementedException();
        }
    }
}
