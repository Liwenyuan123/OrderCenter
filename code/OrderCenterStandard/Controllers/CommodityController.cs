
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

namespace OrderCenterStandard.Controllers
{
    public class CommodityController : ApiController
    {
        //Get api/Commodity
        [HttpGet]
        public IHttpActionResult Get(string ComName, int TypeID, int PageIndex)
        {
            //string ComName,int TypeID,int PageIndex
            var comService = new CommodityService();
            int PageCount = 0;
            int PageTotal = 0;
            var lists = comService.Select(ComName, TypeID, PageIndex,out PageCount,out PageTotal,PageSize.Count);
            var reModel = new { Msg = "查询成功", code = 0, data = lists };
            return Json(reModel);
        }
        //app商品列表页
        [HttpGet]
        public IHttpActionResult Get()
        {
            var comService = new CommodityService();
            var list = comService.GroupByType();
            return Json(list);
         }
        //Post api/Commodity
        public IHttpActionResult Post([FromBody]dynamic query,[FromUri] int flagType)
        {
            string str =Convert.ToString( query);
            CommodityViewModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<CommodityViewModel>(str);
            var comService = new CommodityService();
            switch (flagType)
            {
                case 1:
                    comService.Add(model);break;
                case 2:
                    comService.Update(model);break;

            }
            return Ok("操作成功！");
            
        }
        //Post api/Commodity/UID=?FlagType =1
        public IHttpActionResult Post(string UID,int FlagType)
        {
            var reMsg="";
            CommodityService comService = new CommodityService();
            switch (FlagType)
            {
                case 1:
                  if (comService.Delete(UID))
                        reMsg="操作成功！";
                    else reMsg ="操作失败";break;
                case 2:
                    reMsg = Newtonsoft.Json.JsonConvert.SerializeObject( comService.GetByID(UID)).ToString();
                    break;
                default:
                    break;
            }
            return Ok(reMsg);
        }
    }
}
