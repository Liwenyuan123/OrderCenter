using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenter.Data.DTO.ViewEnum;
using Newtonsoft.Json;
using System.Web;

namespace OrderCenterStandard.Controllers
{
    public class ServiceController : ApiController
    {

        /// <summary>
        /// 根据用户名获取token
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetToken(string staffId)
        {
            ResultMsg resultMsg = null;
            int id = 0;

            //判断参数是否合法
            if (string.IsNullOrEmpty(staffId) || (!int.TryParse(staffId, out id)))
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
                resultMsg.Info = StatusCodeEnum.ParameterError.GetEnumText();
                resultMsg.Data = "";
                return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
            }

            //查询缓存
            Token token = (Token)HttpRuntime.Cache.Get(id.ToString());
            if (HttpRuntime.Cache.Get(id.ToString()) == null)
            {
                token = new Token();
                token.StaffId = id;
                token.SignToken = Guid.NewGuid();
                token.ExpireTime = DateTime.Now.AddDays(1);
                HttpRuntime.Cache.Insert(token.StaffId.ToString(), token, null, token.ExpireTime, TimeSpan.Zero);
            }

            //返回token信息
            resultMsg = new ResultMsg();
            resultMsg.StatusCode = (int)StatusCodeEnum.Success;
            resultMsg.Info = "";
            resultMsg.Data = token;

            return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
        }


       
    }
}
