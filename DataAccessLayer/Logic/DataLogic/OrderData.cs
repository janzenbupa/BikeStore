﻿using BikeStore.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.DataAccessLayer.Logic.DataLogic
{
    /// <summary>
    /// This class will call the db using a SqlConnection.
    /// </summary>
    public static class OrderData
    {
        /// <summary>
        /// Retrieve a list of Orders from the db.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<Order> RetrieveOrders(string connectionString)
        {
            List<Order> obj = new List<Order>();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Retrieve_Orders]";
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCmd.Connection.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        obj = ReadOrders(reader);
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
        /// Call the SaveOrder stored procedure to save a new order into the db.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Int64 SaveOrder(string connectionString, Order order)
        {
            Int64 obj = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string nonQuery = "[dbo].[SaveOrder]";
                    using (SqlCommand sqlCmd = new SqlCommand(nonQuery, sqlCon))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@Id", order.Id)).Direction = System.Data.ParameterDirection.Output;
                        sqlCmd.Parameters.Add(new SqlParameter("@bikeId", order.BikeId));
                        sqlCmd.Parameters.Add(new SqlParameter("@quantityOfBikes", order.QuantityOfBikes));
                        sqlCmd.Parameters.Add(new SqlParameter("@customerId", order.CustomerId));
                        sqlCmd.Parameters.Add(new SqlParameter("@price", order.Price));

                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();

                        obj = Int64.Parse(sqlCmd.Parameters["@Id"].Value.ToString()) as  Int64? ?? 0;
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
        /// Private method used by RetrieveOrders to save the db results in a list of Orders.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Order> ReadOrders(SqlDataReader reader)
        {
            Order order = new Order();
            List<Order> returnObj = new List<Order>();
            while (reader.Read())
            {
                order.Id = Int64.Parse(reader["Id"].ToString());
                order.BikeId = reader["BikeId"].ToString();
                order.QuantityOfBikes = Int32.Parse(reader["QuantityOfBikes"].ToString());
                order.CustomerId = Int64.Parse(reader["CustomerId"].ToString());

                returnObj.Add(order);
            }

            return returnObj;
        }
    }
}
