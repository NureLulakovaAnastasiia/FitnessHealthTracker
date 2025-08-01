using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain.Entities
{
    public class UserAim: BaseModel
    {
        public Aim Aim { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public float? AimValue { get; set; }
        public bool IsAchieved { get; set; } = false;

        public UserAim()  { }
        public UserAim(Aim aim, DateTime? endDate, float? aimValue)
        {
            Aim = aim;
            EndDate = endDate == null ? DateTime.UtcNow.AddMonths(1) : endDate.Value;
        }
    }

}
