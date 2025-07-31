using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class WeightParameter: BaseModel
    {
        public float Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
