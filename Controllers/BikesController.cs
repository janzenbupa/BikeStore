using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.DataAccessLayer.Logic.BikeLogic;
using BikeStore.Models.Orders;
using BikeStore.Models.Orders.GetModels;
using BikeStore.Models.Orders.PostModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        [HttpGet]
        public GetBaseResponse Get()
        {
            GetBaseResponse returnObj = new GetBaseResponse { };
            try
            {
                BikeManager manager = new BikeManager();
                string bikes = manager.GetBikes();

                returnObj.Timestamp = DateTime.Now;
                returnObj.Body = bikes;
            }
            catch (Exception ex)
            {
                returnObj.Body = ex.Message;
                returnObj.Timestamp = DateTime.Now;
            }

            return returnObj;
        }

        [HttpPost]
        public BaseResponse Post(BikeBaseRequest bikeRequest)
        {
            BaseResponse returnObj = new BaseResponse();
            try
            {
                BikeManager manager = new BikeManager();
                string response = manager.AddBike(bikeRequest);

                returnObj.Body = response;
                returnObj.Timestamp = DateTime.Now;
                return returnObj;
            }
            catch (Exception ex)
            {
                returnObj.Body = ex.Message;
                returnObj.Timestamp = DateTime.Now;
                return returnObj;
            }
        }
    }
}
