using BikeStore.Configuration;
using BikeStore.DataAccessLayer.Logic.DataLogic;
using BikeStore.DataAccessLayer.Models;
using BikeStore.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.DataAccessLayer.Logic.BikeLogic
{
    public class BikeManager
    {
        public String GetBikes()
        {
            string returnObj;

            try
            {
                List<Bike> bikes = BikeData.RetrieveBikes(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"));

                returnObj = Newtonsoft.Json.JsonConvert.SerializeObject(bikes);

                return returnObj;
            }
            catch (Exception ex)
            {
                return returnObj = ex.Message;
            }
        }
        public String AddBike(BikeBaseRequest bikeRequest)
        {
            string returnObj;

            try
            {
                if (String.IsNullOrEmpty(bikeRequest.Model))
                {
                    returnObj = "Bike model cannot be empty.";
                    return returnObj;
                }
                if (bikeRequest.Price <= 0)
                {
                    returnObj = "Enter a valid price.";
                    return returnObj;
                }
                if (bikeRequest.Quantity <= 0)
                {
                    returnObj = "Quantity must be greater than 0.";
                    return returnObj;
                }

                Bike bike = new Bike
                {
                    Model = bikeRequest.Model,
                    Price = bikeRequest.Price,
                    Quantity = bikeRequest.Quantity
                };

                long id = BikeData.SaveBike(new ConfigurationRetriever().RetrieveConfig("ConnectionStrings", "BikeStore"), bike);

                if (id >= 1)
                {
                    returnObj = "Successfully saved bike.";
                    return returnObj;
                }

                returnObj = "Failed to save bike.";
                return returnObj;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
