using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public class CaloriesPerDayDto
    {
        public float TotalBurnedCalories {
            get
            {
                return Activities.Sum(a => a.CaloriesValue);
            }
        
        }

        public float TotalGainedCalories
        {
            get
            {
                return Meals.Sum(a => a.CaloriesValue);
            }

        }
        public DateTime Date {  get; set; }
        public ICollection<CaloriesPerActivityDto> Activities { get; set; }
        public ICollection<CaloriesPerMealDto> Meals { get; set; }
    }
}
