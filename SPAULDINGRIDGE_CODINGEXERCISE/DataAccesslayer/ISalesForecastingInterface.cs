using SPAULDINGRIDGE_CODINGEXERCISE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPAULDINGRIDGE_CODINGEXERCISE.DataAccesslayer
{
  public  interface ISalesForecastingInterface
    {
         List<YearForecastingModel> GetSalesData(int year, float increment);
        List<YearForecastingModel> GetChartSalesData(int year);
        List<YearForecastingModel> GetStateChartSalesData(int year);
    }
}
