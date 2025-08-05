using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public class UserAimDto
    {
        public int Id { get; set; }
        public int AimId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public float? AimValue { get; set; }
        public string UserId { get; set; }
        public bool IsAchieved { get; set; } = false;
    }
}
