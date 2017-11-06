using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.DTO;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO.CommHelper;

namespace OrderCenterStandard.Controllers
{
    public class app_UserController : ApiController
    {
        UserService service = new UserService();
        // POST: api/UserLogin
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //check args
            string loginId = query.LoginId;
            string secretString = query.SecretMessage;
            int random = query.random;
            //long timeStamp = query.timeStamp;
            string Msg = "登录失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            //login and get user info

            //support loginid,phone,email login!
            UserInfoSelfViewModel model = service.app_UserLogin(loginId, secretString, out Msg, out Code);
            List<UserInfoSelfViewModel> list = new List<UserInfoSelfViewModel>();
            list.Add(model);
            return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0, 0, 0, Msg, Code, list));
        }



    }
}
