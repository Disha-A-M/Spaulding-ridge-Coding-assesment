using Microsoft.AspNetCore.Mvc;
using SPAULDINGRIDGE_CODINGEXERCISE.DataAccesslayer;
using SPAULDINGRIDGE_CODINGEXERCISE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPAULDINGRIDGE_CODINGEXERCISE.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SalesForecastingController : Controller
    {
        public ISalesForecastingInterface _salesForecasting;
        public SalesForecastingController(ISalesForecastingInterface salesForecastingInterface)
        {
            _salesForecasting = salesForecastingInterface;
        }

        [HttpGet]
        public List<YearForecastingModel> GetSalesData(int year, float increment)
        {
            var data = _salesForecasting.GetSalesData(year, increment);
            return data;

        }
        [HttpGet]
        public List<YearForecastingModel> GetchartSalesData(int year)
        {
            return _salesForecasting.GetChartSalesData(year);
        }
        [HttpGet]
        public List<YearForecastingModel> GetSatechartSalesData(int year)
        {
            return _salesForecasting.GetStateChartSalesData(year);
        }
    }
}
