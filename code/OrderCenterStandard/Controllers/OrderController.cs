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
using SF_Frame_Valudation;

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
                case "发货":
                    re=service.UpdateOrderState(MainID, (int)OrderState.IsOver);
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
            
            service.AddOrder(orderMain, orderDetails);

            return Ok();
        }
    }
}
