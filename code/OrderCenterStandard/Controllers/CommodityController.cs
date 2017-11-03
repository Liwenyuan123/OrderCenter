
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

namespace OrderCenterStandard.Controllers
{
    public class CommodityController : ApiController
    {
        //Get api/Commodity
        [HttpPost]
        public IHttpActionResult Post(string ComName, int TypeID, int PageIndex)
        {
            //string ComName,int TypeID,int PageIndex
            var comService = new CommodityService();
            int PageCount = 0;
            int PageTotal = 0;
            string Msg = "操作失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            var lists = comService.Select(ComName, TypeID, PageIndex, out PageCount, out PageTotal, PageSize.Count, out Code, out Msg);
            return Json(new Return_ResultJsonModel<CommodityViewModel>(PageIndex, PageCount, PageTotal, Msg, Code, lists));
        }
        //app商品列表页
        [HttpGet]
        public IHttpActionResult Get()
        {
            var comService = new CommodityService();
            var list = comService.GroupByType();

            return Json(new Return_ResultJsonModel<dynamic>(0, 0, 0, "查询成功", (int)ReturnCode.OK, list));
        }
        //Post api/Commodity
        public IHttpActionResult Post([FromBody]dynamic query, [FromUri] int FlagType)
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
        //Post api/Commodity/UID=?FlagType =1
        public IHttpActionResult Post(string UID, int FlagType)
        {
            var Msg = "操作失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            CommodityService comService = new CommodityService();
            switch (FlagType)
            {
                case 1:
                    if (comService.Delete(UID))
                        Msg = "操作成功！";Code = (int)ReturnCode.OK; break;
                case 2:
                    var model = comService.GetByID(UID);
                    Msg = "操作成功"; Code = (int)ReturnCode.OK;
                    break;
                default:
                    break;
            }
            return Ok(new Return_ResultJsonModel<CommodityViewModel>(0,0,0,Msg,Code,null));
        }
    }
}
