using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.DataAccessLayer.Logic.OrderLogic;
using BikeStore.Models.Orders;
using BikeStore.Models.Orders.GetModels;
using BikeStore.Models.Orders.PostModels;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        [HttpGet]
        public GetBaseResponse Get()
        {
            GetBaseResponse returnObj = new GetBaseResponse { };
            try
            {
                OrderManager manager = new OrderManager();
                string orders = manager.GetOrders();

                returnObj.Timestamp = DateTime.Now;
                returnObj.Body = orders;
            }
            catch (Exception ex)
            {
                returnObj.Body = ex.Message;
                returnObj.Timestamp = DateTime.Now;
            }

            return returnObj;
        }

        [HttpPost]
        public BaseResponse Post(OrderBaseRequest orderRequest)
        {
            BaseResponse returnObj = new BaseResponse();
            try
            {
                OrderManager manager = new OrderManager();
                string response = manager.PlaceOrder(orderRequest);

                returnObj.Timestamp = DateTime.Now;
                returnObj.Body = response;
            }
            catch (Exception ex)
            {
                returnObj.Body = ex.Message;
                returnObj.Timestamp = DateTime.Now;
            }

            return returnObj;
        }
    }
}
