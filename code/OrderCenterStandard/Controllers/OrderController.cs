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

namespace OrderCenterStandard.Controllers
{
    public class OrderController : ApiController
    {
        //api/Order
        public IHttpActionResult Get()
        {
            string jsonArrayText1 = "[{'a':'a1','b':'b1'},{'a':'a2','b':'b2'}]";
            
            JArray ja = (JArray)JsonConvert.DeserializeObject(jsonArrayText1);
            string ja1a = ja[1]["a"].ToString();
            List<Demo> list = new List<Demo>();
            for(int i = 0; i < ja.Count; i++)
            {
                Demo demo = new Demo();
                demo.id =( i + 1);
                demo.value = ja[i].ToString();
                list.Add(demo);
            }
            //或者
            JObject o = (JObject)ja[1];
            string oa = o["a"].ToString();
            return Ok();
        }
        public class Demo
        {
            public int id { get; set; }
            public string value { get; set; }
        }


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        //api/Order
        public IHttpActionResult Post([FromBody]dynamic query)
        {
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
