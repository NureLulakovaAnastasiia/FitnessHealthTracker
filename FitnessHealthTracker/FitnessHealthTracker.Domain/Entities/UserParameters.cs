using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class UserParameters: BaseModel
    {
        public DateTime DateOfBirthday { get; set; }
        public int Height { get; set; }

        public int Age
        {
            get
            {
                var year = DateTime.UtcNow.Year - DateOfBirthday.Year;
                if (DateTime.UtcNow.Month < DateOfBirthday.Month
                    || DateTime.UtcNow.Month == DateOfBirthday.Month && DateTime.UtcNow.Day < DateOfBirthday.Day)
                {
                    year--;
                }
                return year;
            }
        }

        public UserParameters()
        {
            DateOfBirthday = DateTime.MinValue;
            Height = 0;
        }
    }
}
