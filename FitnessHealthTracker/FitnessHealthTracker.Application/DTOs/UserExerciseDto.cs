using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public class UserExerciseDto
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Calories { get; set; }
        public string UserId { get; set; }
    }
}
