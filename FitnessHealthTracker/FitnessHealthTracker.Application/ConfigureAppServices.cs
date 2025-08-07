using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Application.IService;
using FitnessHealthTracker.Application.Mappings;
using FitnessHealthTracker.Application.Service;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace FitnessHealthTracker.Application
{
    public static class ConfigureAppServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IAimService, AimService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHealthParametersService, HealthParameterService>();
            services.AddScoped<IUserStatisticsService, UserStatisticsService>();

            return services;
        }
    }
}
