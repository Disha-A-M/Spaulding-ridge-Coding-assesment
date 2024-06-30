using System;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using SPAULDINGRIDGE_CODINGEXERCISE.Models;

namespace SPAULDINGRIDGE_CODINGEXERCISE.DataAccesslayer
{
    public class SalesForecastingDataAccess: ISalesForecastingInterface
    {
        public IConfiguration _configuration;
        private SqlConnection conn;
        public SalesForecastingDataAccess(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public SqlConnection GetConnection()
        {
            conn = new SqlConnection(_configuration.GetSection("Data").GetSection("ConnectionStrings").Value);
            return conn;
        }
        public List<YearForecastingModel> GetSalesData(int year, float increment)
        {
            SqlConnection connection = GetConnection();
            connection.Open();
                SqlTransaction transcation = null;
                try
                {
                List<YearForecastingModel> forecastingModels = new List<YearForecastingModel>();
                if (connection != null)
                { 
                        if (connection.State == ConnectionState.Open)
                        {
                            SqlCommand cmd = new SqlCommand();
                            SqlDataReader read = null;
                            cmd.Transaction = conn.BeginTransaction();
                            transcation = cmd.Transaction;
                            cmd.Connection = connection;
                            cmd.CommandText = "GET_TOTAL_SALES";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@YEAR", year);
                        cmd.Parameters.AddWithValue("@INCREMENT", increment);
                        read = cmd.ExecuteReader();
                           
                            while (read.Read())
                            {
                                YearForecastingModel yearForecastingModel = new YearForecastingModel();
                                yearForecastingModel.State = (string)read["STATE"];
                                yearForecastingModel.TotalSales = (decimal)read["TOTAL_SALES"];
                                yearForecastingModel.PercentageIncrease = (decimal)read["INCREMENTED_SALES"];
                                forecastingModels.Add(yearForecastingModel);
                            }
                        }
                    
                }
                return forecastingModels;
            }
                catch (Exception e)
                {
                    throw e;

                }
        }

        public List<YearForecastingModel> GetChartSalesData(int year)
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            SqlTransaction transcation = null;
            try
            {
                List<YearForecastingModel> forecastingModels = new List<YearForecastingModel>();
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader read = null;
                        cmd.Transaction = conn.BeginTransaction();
                        transcation = cmd.Transaction;
                        cmd.Connection = connection;
                        cmd.CommandText = "GET_CHART_DATA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@YEAR", year);
                        read = cmd.ExecuteReader();

                        while (read.Read())
                        {
                            YearForecastingModel yearForecastingModel = new YearForecastingModel();
                            yearForecastingModel.YearType = (string)read["YearType"];
                            yearForecastingModel.Year = (int)read["Year"];
                            yearForecastingModel.TotalSales = (decimal)read["Sales"];
                            forecastingModels.Add(yearForecastingModel);
                        }
                    }

                }
                return forecastingModels;
            }
            catch (Exception e)
            {
                throw e;

            }

        }
        public List<YearForecastingModel> GetStateChartSalesData(int year)
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            SqlTransaction transcation = null;
            try
            {
                List<YearForecastingModel> forecastingModels = new List<YearForecastingModel>();
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader read = null;
                        cmd.Transaction = conn.BeginTransaction();
                        transcation = cmd.Transaction;
                        cmd.Connection = connection;
                        cmd.CommandText = "GET_CHART_DATA_BASED_ON_SITE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@YEAR", year);
                        read = cmd.ExecuteReader();

                        while (read.Read())
                        {
                            YearForecastingModel yearForecastingModel = new YearForecastingModel();
                            yearForecastingModel.State = (string)read["State"];
                            yearForecastingModel.SeedingSales = (decimal)read["SeedingYearSales"];
                            yearForecastingModel.ForecastedSaled = (decimal)read["ForecastedYearSales"];
                            forecastingModels.Add(yearForecastingModel);
                        }
                    }

                }
                return forecastingModels;
            }
            catch (Exception e)
            {
                throw e;

            }

        }


    }
}
