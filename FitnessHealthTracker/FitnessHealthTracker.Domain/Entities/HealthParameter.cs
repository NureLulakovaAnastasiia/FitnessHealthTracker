using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class HealthParameter : BaseModel
    {
        public float Value { get; set; }
        public DateTime DateTime { get; set; }
        public HealthParameterType Type { get; set; }
        public string UserId { get; set; }
        
    }

    public enum HealthParameterType
    {
        Weight,
        Height,
        Pulse,
        BloodPressure
    }
}
