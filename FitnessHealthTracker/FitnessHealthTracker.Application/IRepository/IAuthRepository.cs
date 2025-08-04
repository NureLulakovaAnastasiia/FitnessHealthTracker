using FitnessHealthTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IRepository
{
    public interface IAuthRepository
    {
        public Task<Result<bool>> Register(string email, string password, string firstName, string lastName);
        public Task<Result<bool>> LogIn(string email, string password);

    }
}
