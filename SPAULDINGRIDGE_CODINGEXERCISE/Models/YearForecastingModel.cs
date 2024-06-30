using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPAULDINGRIDGE_CODINGEXERCISE.Models
{
    public class YearForecastingModel
    {
        public int Year { get; set; }
        public string State { get; set; }
        public decimal TotalSales { get; set; } 
        public decimal PercentageIncrease { get; set; }
        public string YearType { get; set; }
        public decimal SeedingSales { get; set; }
        public decimal ForecastedSaled { get; set; }
    }
}
