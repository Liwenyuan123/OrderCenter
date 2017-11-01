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
    public class app_OrderController : ApiController
    {
        OrderService service = new OrderService();

        //get the history OrderList
        //api/Order/
        [HttpGet]
        public IHttpActionResult Get(string SearchC ,string Flag)
        {
            
            switch (Flag)
            {
                case "History":
                    var models = service.GetOrderByUserID(SearchC);return Json(models);
                case "FuzzyS":
                    var comList = service.app_FuzzySearch(SearchC);return Json(comList);
                default:
                    break;
            }
            return Json("无数据");
        }
        //submit order
        [HttpPost]
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            string Msg = "";
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<app_OrderMain>(query);
            bool result = service.app_AddOrder(model,out Msg);
          
            return Json(Msg);
        }
        [HttpGet]
        public IHttpActionResult Get(string ComName)
        {
            if(ComName != null && ComName != "")
            {
                var model = service.app_FuzzySearch(ComName);
                return Json(model);
            }
            else
            {
                return Json("请输入查询条件");
            }
        }
    }
}
