using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class Exercise: BaseModel
    {
        public string Name { get; set; }

        public Exercise(string name)
        {
            Name = name;
        }
    }
}
