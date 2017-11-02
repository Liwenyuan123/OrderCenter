using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SF_Frame_Valudation;
using OrderCenter.Data.DTO;
using OrderCenter.Data.Service;

namespace OrderCenterStandard.Controllers
{
    public class app_UserController : ApiController
    {
        UserService service = new UserService();
        // POST: api/UserLogin
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //check args
            SF_Frame_Valudation.CheckDataNullValudation.CheckNullGet(query, "数据为空");
            string loginId = query.LoginId;
            string secretString = query.SecretMessage;
            int random = query.random;
            //long timeStamp = query.timeStamp;
            string Msg = "登录失败";
            //login and get user info

            //support loginid,phone,email login!
             UserInfoSelfViewModel model = service.app_UserLogin(loginId, secretString,out Msg);
            var re = new { Message = Msg, Data = model };
            return Json(re);
        }



    }
}
