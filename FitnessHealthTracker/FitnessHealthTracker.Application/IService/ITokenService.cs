using FitnessHealthTracker.Application.Service;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.IService
{
    public interface ITokenService
    {
        public Result<string> GenerateToken(User user);
    }
}
