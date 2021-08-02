using BikeStore.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BikeStore.DataAccessLayer.Logic.DataLogic
{
    /// <summary>
    /// This class will call the db using a SqlConnection.
    /// </summary>
    public static class BikeData
    {
        /// <summary>
        /// Retrieve a list of Bikes from the db.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<Bike> RetrieveBikes(string connectionString)
        {
            List<Bike> obj = new List<Bike>();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Retrieve_Bikes]";
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCmd.Connection.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        obj = ReadBikes(reader);
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call the SaveBike stored procedure to save a new bike into the db.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="bike"></param>
        /// <returns></returns>
        public static Int64 SaveBike(string connectionString, Bike bike)
        {
            Int64 obj = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string nonQuery = "[dbo].[SaveBike]";
                    using (SqlCommand sqlCmd = new SqlCommand(nonQuery, sqlCon))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@Id", bike.Id)).Direction = System.Data.ParameterDirection.InputOutput;
                        sqlCmd.Parameters.Add(new SqlParameter("@model", bike.Model));
                        sqlCmd.Parameters.Add(new SqlParameter("@price", bike.Price));
                        sqlCmd.Parameters.Add(new SqlParameter("@quantity", bike.Quantity));
                        sqlCmd.Parameters.Add(new SqlParameter("@available", bike.Available));

                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();

                        obj = Int64.Parse(sqlCmd.Parameters["@Id"].Value.ToString()) as Int64? ?? 0;
                    }
                }
            }
            catch (Exception)
            {
                obj = -1;
            }
            return obj;
        }

        /// <summary>
        /// Private method used by RetrieveBikes to save the db results in a list of Bikes.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Bike> ReadBikes(SqlDataReader reader)
        {
            Bike bike;
            List<Bike> returnObj = new List<Bike>();
            while (reader.Read())
            {
                bike = new Bike();
                bike.Id = Int64.Parse(reader["Id"].ToString());
                bike.Model = reader["Model"].ToString();
                bike.Price = Decimal.Parse(reader["Price"].ToString());
                bike.Quantity = Int32.Parse(reader["Quantity"].ToString());
                bike.Available = Int32.Parse(reader["Available"].ToString());

                returnObj.Add(bike);
            }

            return returnObj;
        }
    }
}
