using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenterStandard.Services;
using Newtonsoft.Json;
using OrderCenterStandard.ViewModels;
using Newtonsoft.Json.Linq;
using SF_Frame_Valudation;

namespace OrderCenterStandard.Controllers
{
    public class OrderController : ApiController
    {
        //api/Order/id
        public IHttpActionResult Get(string id)
        {
           
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
            OrderView orderViews = JsonConvert.DeserializeObject<OrderView>(str);
            List<O_OrderDetail> orderDetails = new List<O_OrderDetail>();
            O_OrderMain orderMain = new O_OrderMain();

            orderMain.Phone = orderViews.Phone;
            orderMain.Remark = orderViews.Remark;
            orderMain.State = 1;
            orderMain.UsePersonName = orderViews.UsePersonName;
            foreach (var detail in orderViews.OrderDetail)
            {
                O_OrderDetail d = new O_OrderDetail();
                d.CommodityId = detail.CommodityId;
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
