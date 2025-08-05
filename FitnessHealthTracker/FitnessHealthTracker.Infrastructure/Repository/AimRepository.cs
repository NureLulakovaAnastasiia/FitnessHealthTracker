using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using FitnessHealthTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Infrastructure.Repository
{
    public class AimRepository : IAimRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public AimRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddAim(Aim aim)
        {
            _dbContext.Add(aim);
            return _dbContext.SaveChanges() != 0;
        }

        public bool AddUserAim(UserAim userAim, string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.Aims = new List<UserAim>() { userAim};
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteAim(int aimId)
        {
            var aimToDelete = await _dbContext.Aims.FirstOrDefaultAsync(a => a.Id == aimId);
            if (aimToDelete != null)
            {
                _dbContext.Aims.Remove(aimToDelete);
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteUserAim(int aimId)
        {
            var aimToDelete = await _dbContext.UserAims.FirstOrDefaultAsync(a => a.Id == aimId);
            if (aimToDelete != null)
            {
                _dbContext.UserAims.Remove(aimToDelete);
            }
            return _dbContext.SaveChanges() != 0;
        }

        public async Task<ICollection<Aim>> GetAllAims()
        {
            return await _dbContext.Aims.ToListAsync();
        }

        public async Task<ICollection<UserAim>?> GetAllUserAims(string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Aims)
                .FirstOrDefaultAsync();
            if(user != null && user.Aims != null)
            {
                return user.Aims;
            }
            return null;
        }

        public async Task<bool> MarkUserAimAchieved(int userAimId)
        {
            var aimToChange = await _dbContext.UserAims.FirstOrDefaultAsync(a => a.Id == userAimId);
            if (aimToChange != null)
            {
                aimToChange.IsAchieved = true;
               return _dbContext.SaveChanges() != 0;
            }
            return false;
        }

        public bool UpdateAim(Aim aim)
        {
            _dbContext.Update(aim);
            return _dbContext.SaveChanges() != 0;
        }

        public bool UpdateUserAim(UserAim userAim)
        {
            _dbContext.Update(userAim);
            return _dbContext.SaveChanges() != 0;

        }
    }
}
