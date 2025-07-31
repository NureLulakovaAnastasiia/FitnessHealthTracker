using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class Aim: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public Aim(string name) 
        { 
            Name = name;
        }
    }
}
