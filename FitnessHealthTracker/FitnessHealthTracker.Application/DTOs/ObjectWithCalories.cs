using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public abstract class ObjectWithCalories
    {
        public int Id { get; set; } //mealHistory or UserExercise Id
        public string Name { get; set; }
        public int CaloriesValue { get; set; }
        public DateTime Date { get; set; }

    }
}
