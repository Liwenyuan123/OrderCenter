using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SF_Frame_Valudation;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenter.Data.Service;

namespace OrderCenterStandard.Controllers
{
    public class UserLoginController : ApiController
    {
        UserService serivice = new UserService();
        

        // POST: api/UserLogin
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //check args
            SF_Frame_Valudation.CheckDataNullValudation.CheckNullGet(query, "数据为空");
            string loginId = query.LoginId;
            //int random = query.random;
            //long timeStamp = query.timeStamp;
            string secretString =query.SecretString;
            string Msg = "登录失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            //login and get user info
           
           //support loginid,phone,email login!
             UserInfoSelfViewModel loginDataModel = serivice.app_UserLogin(loginId, secretString,out Msg,out Code);
            List<UserInfoSelfViewModel> list = new List<UserInfoSelfViewModel>();
            list.Add(loginDataModel);
            return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0,0,0,Msg,Code,list));
        }

       
    }
}
