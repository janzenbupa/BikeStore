using BikeStore.DataAccessLayer.Models;
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
    public static class CustomerData
    {
        /// <summary>
        /// Retrieve a list of Customers from the db.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<Customer> RetrieveCustomers(string connectionString)
        {
            List<Customer> obj = new List<Customer>();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Retrieve_Customers]";
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCmd.Connection.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        obj = ReadCustomers(reader);
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
        /// Call the SaveCustomer stored procedure to save a new customer into the db.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Int64 SaveCustomer(string connectionString, Customer customer)
        {
            Int64 obj = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string nonQuery = "[dbo].[SaveCustomer]";
                    using (SqlCommand sqlCmd = new SqlCommand(nonQuery, sqlCon))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@Id", customer.Id)).Direction = System.Data.ParameterDirection.InputOutput;
                        sqlCmd.Parameters.Add(new SqlParameter("@firstName", customer.FirstName));
                        sqlCmd.Parameters.Add(new SqlParameter("@lastName", customer.LastName));
                        sqlCmd.Parameters.Add(new SqlParameter("@numberOfOrders", customer.NumberOfOrders));

                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();

                        obj = Convert.ToInt64(sqlCmd.Parameters["@Id"].Value.ToString());
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
        /// Private method used by RetrieveCustomers to save the db results in a list of Customers.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Customer> ReadCustomers(SqlDataReader reader)
        {
            Customer customer = new Customer();
            List<Customer> returnObj = new List<Customer>();
            while (reader.Read())
            {
                customer.Id = Int64.Parse(reader["Id"].ToString());
                customer.FirstName = reader["FirstName"].ToString();
                customer.LastName = reader["LastName"].ToString();
                customer.NumberOfOrders = Int32.Parse(reader["NumberOfOrders"].ToString());

                returnObj.Add(customer);
            }

            return returnObj;
        }
    }
}
