using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderCenterStandard.Controllers
{
    public class GetTokenController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetToken(string staffId)
        {
            HttpResponseMessage resultMsg = null;
            int id = 0;
            //判断参数是否合法
            if(string.IsNullOrEmpty(staffId) ||(!int.TryParse(staffId,out id)))
            {
                resultMsg = new HttpResponseMessage();
                resultMsg.StatusCode = HttpStatusCode.NotFound;
                resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
                resultMsg.Data = "";
                return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
            }
            //插入缓存
            Token token = (Token)HttpRuntime.Cache.Get(id.ToString());
            if (HttpRuntime.Cache.Get(id.ToString()) == null)
            {
                token = new Token();
                token.StaffId = id;
                token.SignToken = Guid.NewGuid();
                token.ExpireTime = DateTime.Now.AddDays(1);
                HttpRuntime.Cache.Insert(token.StaffId.ToString(), token, null, token.ExpireTime, TimeSpan.Zero);
            }

        }
    }
}
