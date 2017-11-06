using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.Service;
using Newtonsoft.Json.Linq;

namespace OrderCenterStandard.Controllers
{
    public class OrderController : ApiController
    {
        OrderService service = new OrderService();
        //api/Order/
        public IHttpActionResult Get(string StartDate,string EndDate,string OrderState,int PageIndex)
        {
            
            int pageCount = 0;
            int pageTotal = 0;
            var models = service.Select(StartDate, EndDate, int.Parse(OrderState), PageIndex, out pageCount, out pageTotal);
            string data = "[{\"PageIndex \":\""+PageIndex+"\",\"PageCount\":\""+pageCount+"\",\"PageTotal\":\""+pageTotal+"\",\"Models\"：\""+Newtonsoft.Json.JsonConvert.SerializeObject(models)+"\"}]";
            return Ok(data);
        }
        //api/Order/id
        public IHttpActionResult Get(string MainId)
        {
            var details = service.GetOrderDetail(MainId);
            return Ok(details);
        }
        //api/Order/MainID=""&&Flag=""
        [HttpGet]
        public IHttpActionResult GetAduit(string MainID,string Flag)
        {
            
            bool re = false;
            switch (Flag)
            {
                case "通过":
                    re= service.UpdateOrderState(MainID,(int) OrderState.AuditPassed);
                    break;
                case "不通过":
                    re= service.UpdateOrderState(MainID, (int)OrderState.AuditPassed);
                    break;
                case "收货":
                    re = service.UpdateOrderState(MainID, (int)OrderState.IsOver);
                    break;

            }
            if (re) { return Ok("操作成功！"); }
            return Ok("操作失败！");
        }
      


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        //api/Order
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            string Msg = "操作失败";
            //string str = Convert.ToString(query);
            OrderMainViewModel orderViews = JsonConvert.DeserializeObject<OrderMainViewModel>(Convert.ToString( query));

            //adding necessary condition judgment......
            if (service.pc_AddOrder(orderViews)) Msg = "操作成功";

            return Ok(Msg);
        }
    }
}
