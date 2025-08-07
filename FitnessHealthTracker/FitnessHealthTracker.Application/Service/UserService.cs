using AutoMapper;
using FitnessHealthTracker.Application.DTOs;
using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Domain;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog;
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
        private readonly ILogger<UserService> _logger;


        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
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
                    Log.Error("Error during getting user '{userId}'", userId);

                }
            }
            catch (Exception ex)
            {
                result.Error = Errors.NoDataFoundMessage;
                Log.ForContext("Id", userId)
                .Error(ex, "Error during getting user");


            }
            return result;
        }

        public async Task<Result<UserParameters>> GetUserParameters(string userId)
        {
            var result = await _userRepository.GetUserParameters(userId);

            return result;
        }

        public async Task<Result<bool>> UpdateUser(GetUserDto user)
        {
            var res = new Result<bool>() { Value = false };

            try
            {
                var userToUpdate = await _userRepository.GetUserById(user.Id);
                if (userToUpdate != null)
                {
                    userToUpdate.Email = user.Email;
                    userToUpdate.FirstName = user.FirstName;
                    userToUpdate.LastName = user.LastName;
                    res.Value = _userRepository.UpdateUser(userToUpdate);
                    _logger.LogInformation("User '{Id}' was updated",user.Id);

                }
            }
            catch (Exception ex)
            {
                res.Error = ex.Message;
                Log.ForContext("Id", user.Id)
                    .Error(ex, "Error during updating user");

            }

            return res;
        }

        public async Task<Result<bool>> UpdateUserParameters(UserParameters userParameters, string userId)
        {
            var result = new Result<bool>() { Value = false };
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user != null)
                {
                    //check if user updates his own parameters
                    var existingParameters = await _userRepository.GetUserParameters(userId);
                    if (existingParameters.IsSuccess && existingParameters.Value.Id == userParameters.Id)
                    {
                        result.Value = _userRepository.UpdateUserParameters(userParameters);
                        _logger.LogInformation("User '{Id}' parameters was updated", user.Id);

                    }

                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                Log.ForContext("Id", userId)
                .Error(ex, "Error during updating user");

            }

            return result;
        }
    }
}
