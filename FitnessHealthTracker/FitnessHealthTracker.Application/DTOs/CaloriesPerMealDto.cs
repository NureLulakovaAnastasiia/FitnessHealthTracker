using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public class CaloriesPerMealDto: ObjectWithCalories
    {
        public int MealWeight { get; set; }
    }
}
