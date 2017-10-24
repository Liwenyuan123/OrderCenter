using OrderCenterStandard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenterStandard.ViewModels;
using OrderCenterStandard.App_Start;

namespace OrderCenterStandard.Controllers
{
    public class CommodityController : ApiController
    {
        //Get api/Commodity
        public IHttpActionResult Get()
        {
            var comService = new CommodityService();
            var lists = comService.GetAll(); 
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(lists));
        }
        //Post api/Commodity
        public IHttpActionResult Post([FromBody]dynamic query,[FromUri] string flagType)
        {
            string str =Convert.ToString( query);
            CommodityView model = Newtonsoft.Json.JsonConvert.DeserializeObject<CommodityView>(str);
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
    }
}
