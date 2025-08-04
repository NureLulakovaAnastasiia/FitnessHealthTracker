using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetUserDto>> GetUser(string userId)
        {
            var result = new Result<GetUserDto>();

            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user != null)
                {
                    result.Value = _mapper.Map<GetUserDto>(user);
                }
                else
                {
                    result.Error = Errors.NoDataFoundMessage;
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public ICollection<Meal> GetUserMeals()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UserParameters>> GetUserParameters(string userId)
        {
            var result = await _userRepository.GetUserParameters(userId);
            return result;
        }

        public bool UpdateUserParameters(UserParameters userParameters)
        {
            throw new NotImplementedException();
        }
    }
}
