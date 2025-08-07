using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class MealHistory: BaseModel
    {
        public int MealId { get; set; }
        public Meal? Meal { get; set; }
        public DateTime Date { get; set; }
        public int WeightInGrams { get; set; }
        public string UserId { get; set; }
        public MealHistory() { }
        public MealHistory(int weight)
        {
            Meal = new Meal();
            WeightInGrams = weight;
        }
    }
}
