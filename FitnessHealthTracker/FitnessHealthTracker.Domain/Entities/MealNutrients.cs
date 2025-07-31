using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class MealNutrients: BaseModel
    {
        public int CaloriesPerHundredGrams { get; set; }
        public float ProteinsPerHundredGrams { get; set; }
        public float FatsPerHundredGrams { get; set; }
        public float CarbohydratesPerHundredGrams { get; set; } //вуглеводи

    }
}
