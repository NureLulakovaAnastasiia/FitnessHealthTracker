using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class Meal: BaseModel
    {
        public string Name { get; set; }
        public MealNutrients Nutrients { get; set; }

        public Meal(int id, string name, MealNutrients nutrients)
        {
            Id = id;
            Name = name;
            Nutrients = nutrients;
        }
    }
}
