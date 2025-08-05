using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using FitnessHealthTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dbContext;

        public UserRepository(UserManager<User> userManager, ApplicationDBContext dBContext)
        {
            _userManager = userManager;
            _dbContext = dBContext;
        }
        public async Task<User?> GetUserByEmail(string userEmail)
        {
            return  await _userManager.FindByEmailAsync(userEmail);
        }

        public async Task<User?> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);

        }

        public async Task<Result<UserParameters>> GetUserParameters(string userId)
        {
            var result = new Result<UserParameters>();

            try
            {
                var user = await _dbContext.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Parameters)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    result.Error = Errors.NoDataFoundMessage;
                    return result;
                }

                result.Value = user.Parameters;
            } catch (Exception ex) 
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public bool UpdateUser(User user)
        {
            _dbContext.Update(user);
            return _dbContext.SaveChanges() != 0;

        }

        public bool UpdateUserParameters(UserParameters userParameters)
        {
            var res =  _dbContext.Update(userParameters);
            return  _dbContext.SaveChanges() != 0;
        }
    }
}
