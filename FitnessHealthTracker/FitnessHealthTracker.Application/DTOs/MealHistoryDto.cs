using FitnessHealthTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Application.DTOs
{
    public class MealHistoryDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int WeightInGrams { get; set; }

    }
}
