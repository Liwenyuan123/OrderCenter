using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using OrderCenter.Data.DTO;
using OrderCenter.Data.Service;
using Newtonsoft.Json.Linq;
using SF_Frame_Valudation;

namespace OrderCenterStandard.Controllers
{
    public class OrderController : ApiController
    {
        //api/Order/id
        public IHttpActionResult Get(string id)
        {
            //this is a text
            Guid.Parse(id);
            OrderService service = new OrderService();
           
            return Ok();
        }
      


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        //api/Order
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            CheckDataNullValudation.CheckNullGet(query,"请输入参数");
            string str = Convert.ToString(query);
            OrderMainViewModel orderViews = JsonConvert.DeserializeObject<OrderMainViewModel>(str);
            List<O_OrderDetail> orderDetails = new List<O_OrderDetail>();
            O_OrderMain orderMain = new O_OrderMain();

            orderMain.Phone = orderViews.Phone;
            orderMain.Address = orderViews.Address;
            orderMain.State = 1;
            orderMain.UsePersonName = orderViews.UsePersonName;
            foreach (var detail in orderViews.OrderDetail)
            {
                O_OrderDetail d = new O_OrderDetail();
                d.ComName = detail.ComName;
                d.NumPlan = detail.NumPlan;
                d.PricePlan = detail.PricePlan;
                orderDetails.Add(d);
            }
            var service = new OrderService();
            service.AddOrder(orderMain, orderDetails);

            return Ok();
        }
    }
}
