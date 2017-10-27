
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;
using OrderCenterStandard.App_Start;

namespace OrderCenterStandard.Controllers
{
    public class CommodityController : ApiController
    {
        //Get api/Commodity
        public IHttpActionResult Get(string ComName,int TypeID,int PageIndex,int PageCount,int PageTotal)
        {
            var comService = new CommodityService();
            var lists = comService.Select(ComName, TypeID, PageIndex, PageCount,out PageTotal,10); 
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(lists));
        }
        //Post api/Commodity
        public IHttpActionResult Post([FromBody]dynamic query,[FromUri] string flagType)
        {
            string str =Convert.ToString( query);
            CommodityViewModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<CommodityViewModel>(str);
            var comService = new CommodityService();
            switch (flagType)
            {
                case "1":
                    comService.Add(model);break;
                case "2":
                    comService.Update(model);break;

            }
            return Ok("操作成功！");
            
        }
        public IHttpActionResult Post(string UID,string flagType)
        {
            var reMsg="";
            CommodityService comService = new CommodityService();
            switch (flagType)
            {
                case "1":
                  if (comService.Delete(UID))
                        reMsg="操作成功！";
                    else reMsg ="操作失败";break;
                case "2":
                    reMsg = Newtonsoft.Json.JsonConvert.SerializeObject( comService.GetByID(UID)).ToString();
                    break;
                default:
                    break;
            }
            return Ok(reMsg);
        }
    }
}
