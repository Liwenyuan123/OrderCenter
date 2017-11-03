using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.CommHelper;

namespace OrderCenterStandard.Controllers
{
    public class app_RegisterController : ApiController
    {
        UserService service = new UserService();
        // POST: api/UserLogin
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //check args
            app_RegisterViewModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<app_RegisterViewModel>(query);
            string Msg = "true";
            int Code = -1;
            //login and get user info
            //support loginid,phone,email login!
            UserInfoSelfViewModel reModel = service.app_RegisterUser(model, out Msg,out Code);
            List<UserInfoSelfViewModel> mList = new List<UserInfoSelfViewModel>();
            mList.Add(reModel);
            return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0,0,0,Msg,Code, mList));
        }
    }
}
