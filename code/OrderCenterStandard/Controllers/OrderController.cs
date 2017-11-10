using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenter.Data.Service;


namespace OrderCenterStandard.Controllers
{
    public class OrderController : ApiController
    {
        OrderService service = new OrderService();

        [HttpPost]
        [Route("api/Order/GetList")]
        //string StartDate,string EndDate,string OrderState,int PageIndex
        //get orderList 
        public IHttpActionResult GetList([FromBody]dynamic query)
        {

            int pageCount = 0;
            int pageTotal = 0;
            string msg = "查询失败";
            int code = (int)ReturnCode.OPERATION_FAILED;
            var list = service.Select(query.StartDate, query.EndDate, int.Parse(query.OrderState), query.PageIndex, out pageCount, out pageTotal, out msg, out code);

            Return_ResultJsonModel<OrderMainViewModel> reJson = new Return_ResultJsonModel<OrderMainViewModel>(query.PageIndex, pageCount, pageTotal, msg, code, list);
            return Json(reJson);
        }
        //api/Order/id
        [HttpPost]
        [Route("api/Order/GetDetail")]
        public IHttpActionResult GetDetail([FromBody]string MainId)
        {
            var details = service.GetOrderDetail(MainId);
            return Json(new Return_ResultJsonModel<O_OrderDetail>(0,0,0,"查询成功",(int)ReturnCode.OK,details));
        }
        //api/Order/MainID=""&&Flag=""
        [HttpPost]
        public IHttpActionResult GetAduit(dynamic query  )
        {
            string MainID = query.MainID;
            string Flag = query.Flag;
            bool re = false;
            switch (Flag)
            {
                case "Adopt":
                    re = service.UpdateOrderState(MainID, (int)OrderState.AuditPassed);
                    break;
                case "Refuse":
                    re = service.UpdateOrderState(MainID, (int)OrderState.AuditPassed);
                    break;
                case "IsOver":
                    re = service.UpdateOrderState(MainID, (int)OrderState.IsOver);
                    break;

            }
            if (re) { return Json(new Return_ResultJsonModel<OrderDetailViewModel>(0, 0, 0, "操作成功", (int)ReturnCode.OK, null)); }
            return Json(new Return_ResultJsonModel<OrderDetailViewModel>(0, 0, 0, "操作失败", (int)ReturnCode.OPERATION_FAILED, null));
        }



        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        //api/Order
        [HttpPost]
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            string Msg = "操作失败";
            //string str = Convert.ToString(query);
            OrderMainViewModel orderViews = JsonConvert.DeserializeObject<OrderMainViewModel>(Convert.ToString(query));

            //adding necessary condition judgment......
            if (service.pc_AddOrder(orderViews)) Msg = "操作成功";

            return Ok(Msg);
        }
    }
}
