using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public string? Error { get; set; }
        public bool IsSuccess
        {
            get
            {
                return Value != null && Error == null;
            }
        }
    }
}
