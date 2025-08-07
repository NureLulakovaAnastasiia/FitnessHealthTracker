using FitnessHealthTracker.Application.IRepository;
using FitnessHealthTracker.Infrastructure.Data;
using FitnessHealthTracker.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FitnessHealthTracker.Domain.Entities;

namespace FitnessHealthTracker.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer("Data Source=CompNastia\\SQLEXPRESS;Initial Catalog=FitnessHealhTrackerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            //services.AddDbContext<ApplicationDBContext>(options =>
            //options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDBContext>();

            services.AddScoped<IAimRepository, AimRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHealthParametersRepository, HealthParametersRepository>();
            services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();

            return services;
        }
    }
}
