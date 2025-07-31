using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public abstract class ObjectWithCalories
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CaloriesValue { get; set; }

    }
}
