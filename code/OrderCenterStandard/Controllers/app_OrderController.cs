using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.DTO;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenter.Data.DTO.ViewEnum;

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
                    var models = service.GetOrderByUserID(SearchC);
                    Return_ResultJsonModel<O_OrderMain> reJson = new Return_ResultJsonModel<O_OrderMain>(0, 0, 0, "操作成功", 0, models); 
                    return Json(reJson);
                case "FuzzyS":
                    var comList = service.app_FuzzySearch(SearchC);
                    Return_ResultJsonModel<CommodityViewModel> reJsonF = new Return_ResultJsonModel<CommodityViewModel>(0, 0, 0, "操作成功", 0, comList);
                    return Json(reJsonF);
                default:
                    break;
            }
            return Json(new Return_ResultJsonModel<CommodityViewModel>(0, 0, 0, "调用方法错误", 1001, null));
        }
        //submit order
        [HttpPost]
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            string Msg = "";
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<app_OrderMain>(query);
            bool result = service.app_AddOrder(model,out Msg);
            
            return Json(new Return_ResultJsonModel<OrderMainViewModel>(0,0,0,Msg,(int)ReturnCode.OK,null));
        }
       
        
    }
}
