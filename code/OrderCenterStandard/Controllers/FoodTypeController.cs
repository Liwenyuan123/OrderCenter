using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenter.Data.DTO.ViewEnum;

namespace OrderCenterStandard.Controllers
{
    public class FoodTypeController : ApiController
    {
        // GET api/FoodType
        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = new FoodTypeService();
            var lists = service.GetAll();
            return Ok(new Return_ResultJsonModel<FoodTypeViewModel>(0,0,0,"查询成功",(int)ReturnCode.OK,lists));
        }
        // Post: api/FoodType
        public IHttpActionResult Post([FromBody]dynamic query, [FromUri] string flag)
        {
            List<O_FoodType> models = new List<O_FoodType>();
            foreach (var item in query)
            {
                string strItem = Convert.ToString(item);
                O_FoodType m = JsonConvert.DeserializeObject<O_FoodType>(strItem);
                models.Add(m);
            }
            var service = new FoodTypeService();
            bool result = service.Add(models);
            return Ok();
        }

        // PUT: api/FoodType
        public IHttpActionResult Put([FromBody]dynamic query)
        {
            throw new Exception();
        }

        // DELETE: api/FoodType
        public IHttpActionResult Delete([FromBody]dynamic query)
        {
            throw new Exception();
        }
    }
}
