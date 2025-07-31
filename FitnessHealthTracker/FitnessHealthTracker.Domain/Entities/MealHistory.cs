using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class MealHistory: BaseModel
    {
        public Meal Meal { get; set; }
        public DateTime Date { get; set; }
        public int WeightInGrams { get; set; }

        public MealHistory(Meal meal, int weight)
        {
            Meal = meal;
            WeightInGrams = weight;
        }
    }
}
