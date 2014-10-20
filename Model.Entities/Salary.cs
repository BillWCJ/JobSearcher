using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    class Salary
    {
        int JobId { get; set; }
        public double PerWeek { get; set; }
        public decimal PercentConfidence { get; set; }
        public double PerHour(double hoursPerWeek = 37.5)
        {
            return PerWeek/hoursPerWeek;
        }
    }
}
