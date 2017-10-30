using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.DTO;
using OrderCenter.Data.Service;

namespace OrderCenterStandard.Controllers
{
    public class OrderAppController : ApiController
    {
        OrderService service = new OrderService();

        //api/Order/
        public IHttpActionResult Get(string UserID)
        {
            var models = service.GetOrderByUserID(UserID);
            return Ok(models);
        }
    }
}
