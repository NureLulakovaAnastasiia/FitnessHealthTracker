using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FitnessHealthTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessHealthTracker.Infrastructure.Data
{
    public class ApplicationDBContext : IdentityDbContext<User> 
    {
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Aim> Aims { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<HealthParameter> HealthParameters { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealHistory> MealHistories { get; set; }
        public DbSet<MealNutrients> MealNutrients { get; set; }
        public DbSet<UserAim> UserAims { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }
        public DbSet<UserParameters> UserParameters { get; set; }


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
