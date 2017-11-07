
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;
using OrderCenterStandard.App_Start;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenterStandard.Models;
using Newtonsoft.Json.Linq;

namespace OrderCenterStandard.Controllers
{
    public class CommodityController : ApiController
    {

        [Route("api/Commodity/GetComList")]
        [HttpPost]
        public IHttpActionResult GetComList([FromBody]dynamicSelect query)
        {
            //string ComName,int TypeID,int PageIndex
            var comService = new CommodityService();
            int PageCount = 0;
            int PageTotal = 0;
            string Msg = "操作失败";
            string comName = query.ComName;
            int typeID = query.TypeID;
            int PageIndex = query.PageIndex;
            int Code = (int)ReturnCode.OPERATION_FAILED;
            var lists = comService.Select(comName, typeID, PageIndex, out PageCount, out PageTotal, PageSize.Count, out Code, out Msg);
            return Json(new Return_ResultJsonModel<CommodityViewModel>(PageIndex, PageCount, PageTotal, Msg, Code, lists));
        }
        //app商品列表页
        [HttpGet]
        public IHttpActionResult Get()
        {
            var comService = new CommodityService();
            var list = comService.GroupByType();

            return Json(ReturnOK.OK(0, 0, 0, "查询成功", (int)ReturnCode.OK, list));
        }
        [HttpPost]
        [Route("api/Commodity/AddOrUpdate")]
        public IHttpActionResult Post([FromBody]JObject query, [FromUri] int FlagType)
        {
            string str = Convert.ToString(query);
            CommodityViewModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<CommodityViewModel>(str);
            var comService = new CommodityService();
            string Msg = "操作失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            switch (FlagType)
            {
                case 1:
                    comService.Add(model, out Msg, out Code); break;
                case 2:
                    comService.Update(model, out Msg, out Code); break;

            }
            return Json(new Return_ResultJsonModel<CommodityViewModel>(0, 0, 0, Msg, Code, null));

        }
        
        [HttpPost]
        [Route("api/Commodity/DelOrGetByID")]
        public IHttpActionResult DelOrGetByID([FromBody]dynamicGetByID query )
        {
            var Msg = "操作失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            int flagType = query.FlagType;
            string uid = query.UID;
            CommodityService comService = new CommodityService();
            switch (flagType)
            {
                case 1:
                    if (comService.Delete(uid))
                        Msg = "操作成功！";Code = (int)ReturnCode.OK; break;
                case 2:
                    var model = comService.GetByID(uid);
                    Msg = "操作成功"; Code = (int)ReturnCode.OK;
                    List<CommodityViewModel> list = new List<CommodityViewModel>();
                    list.Add(model);
                    return Ok(new Return_ResultJsonModel<CommodityViewModel>(0, 0, 0, Msg, Code, list));
                default:
                    break;
            }
            return Ok(new Return_ResultJsonModel<CommodityViewModel>(0,0,0,Msg,Code,null));
        }
    }
}
