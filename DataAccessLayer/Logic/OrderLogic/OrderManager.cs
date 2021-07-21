using BikeStore.Configuration;
using BikeStore.DataAccessLayer.Logic.DataLogic;
using BikeStore.DataAccessLayer.Models;
using BikeStore.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeStore.DataAccessLayer.Logic.OrderLogic
{
    public class OrderManager
    {
        public String PlaceOrder(OrderBaseRequest orderRequest)
        {
            string returnObj = string.Empty;

            if (String.IsNullOrEmpty(orderRequest.FirstName))
            {
                returnObj = "First name cannot be empty.";
                return returnObj;
            }
            if (String.IsNullOrEmpty(orderRequest.LastName))
            {
                returnObj = "Last name cannot be empty.";
                return returnObj;
            }
            if (String.IsNullOrEmpty(orderRequest.Model))
            {
                returnObj = "Must specify which model.";
                return returnObj;
            }
            if (orderRequest.Quantity == 0)
            {
                returnObj = "Quantity must be greater than 0. Quantity must match number of bikes ordered.";
                return returnObj;
            }

            List<string> models = orderRequest.Model.Split("-").ToList();
            List<Bike> bikes = BikeData.RetrieveBikes(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"));
            decimal price = 0;
            for (int i = 0; i < models.Count; i++)
            {
                Bike bike = bikes.Where(x => x.Model.Replace(" ", "").ToLower() == models[i].Replace(" ", "").ToLower()).FirstOrDefault();
                if (bike == null)
                {
                    returnObj = "Some of these bikes are not in stock. Either add the Bike to the inventory or update the order request to contain valid bikes only.";
                    return returnObj;
                }

                price += bike.Price;
            }

            long customerId;
            Customer customer = CustomerData.RetrieveCustomers(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"))
                .FirstOrDefault(x => x.FirstName.ToLower() == orderRequest.FirstName.ToLower() && x.LastName.ToLower() == orderRequest.LastName.ToLower());

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                if (customer == null)
                {
                    Customer newCustomer = new Customer
                    {
                        FirstName = orderRequest.FirstName,
                        LastName = orderRequest.LastName,
                        NumberOfOrders = 1
                    };

                    customerId = CustomerData.SaveCustomer(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"), newCustomer);

                    if (customerId <= 0)
                    {
                        returnObj = "Could not find or add customer. Order failed to save.";
                        return returnObj;
                    }

                    customer.Id = customerId;
                }

                else
                {
                customer.NumberOfOrders++;
                customerId = CustomerData.SaveCustomer(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"), customer);

                    if (customerId <= 0)
                    {
                        returnObj = "Could not find or add customer. Order failed to save.";
                        return returnObj;
                    }
                }

                Order order = new Order
                {
                    BikeId = orderRequest.Model,
                    QuantityOfBikes = models.Count,
                    CustomerId = customer.Id,
                    Price = price
                };

                long id = OrderData.SaveOrder(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"), order);
                
                if (id <= 1)
                {
                    returnObj = "Order failed to save.";
                }

                returnObj = "Order has been successfully placed.";
            scope.Complete();
        }

            return returnObj;
        }

        public String GetOrders()
        {
            string returnObj;
            List<Order> orders = OrderData.RetrieveOrders(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"));

            returnObj = Newtonsoft.Json.JsonConvert.SerializeObject(orders);
            return returnObj;
        }
    }
}
