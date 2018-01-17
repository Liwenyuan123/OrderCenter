using OrderCenter.Data.DTO.ViewEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.CommHelper;
using System.Web;
using OrderCenterStandard.Models;

namespace OrderCenterStandard.Controllers
{
    public class loginController : ApiController
    {
        UserLogin userService = new UserLogin();
        // POST: api/UserLogin
        [HttpPost]
        [Route("api/login/loginIn")]
        public IHttpActionResult loginIn([FromBody]dynamic query)
        {

            string loginId = query.LoginId;
            string loginPwd = query.PassWord;
            string loginCode = query.Code;
            string error_message = "登录失败";
            int error_code = (int)ReturnCode.OPERATION_FAILED;
            //login and get user info

            //support loginid,phone,email login!
            UserInfoSelfViewModel loginDataModel = userService.app_UserLogin(loginId, loginPwd, out error_message, out error_code);
            List<UserInfoSelfViewModel> list = new List<UserInfoSelfViewModel>();
            if (error_code > 0)
            {
                loginDataModel.Token = GetToken(loginDataModel.UserID);
                
                list.Add(loginDataModel);
            }
            
            return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0, 0, 0, error_message, error_code, list));
        }
        public string GetToken(string LoginID)
        {
                //查询缓存
                Token token = (Token)HttpRuntime.Cache.Get(LoginID.ToString());
                if (HttpRuntime.Cache.Get(LoginID.ToString()) == null)
                {
                    string dateTimeLong = commonMethod.ConvertDateTimeInt(DateTime.Now.AddDays(1)).ToString();
                    token = new Token();
                    token.StaffId = LoginID;
                    token.SignToken = Guid.NewGuid().ToString("N") + dateTimeLong;
                    token.ExpireTime = DateTime.Now.AddDays(1);
                    HttpRuntime.Cache.Insert(token.StaffId.ToString(), token, null, token.ExpireTime, TimeSpan.Zero);

                }
                //else 判断登录是否到期，待续........
            return token.SignToken;
        }
    }
}
