using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.CommHelper;
using OrderCenter.Data.DTO.ViewEnum;

namespace OrderCenterStandard.Controllers
{
    public class UserController : ApiController
    {
        UserService service = new UserService();
       
        //GET:api/User
        [HttpPost]
        [Route("api/User/GetUsers")]
        public IHttpActionResult GetUsers(string LoginId,int State,int PageIndex)
        {
            int pageSize = 0;//每页条数
            int pageTotal = 0;//总页数
            string error_msg = "操作失败";
            int error_code = (int)ReturnCode.OPERATION_FAILED;
            var models = service.SelectUsers(LoginId, State, PageIndex, pageSize, out pageTotal,out error_msg,out error_code);
            
            return Json(new Return_ResultJsonModel<S_User>(PageIndex,pageSize,pageTotal,error_msg,error_code,models));
           
        }
        //POST:api/User
        [HttpPost]
        [Route("api/User/AddUser")]
        public IHttpActionResult AddUser([FromBody]dynamic query)
        {
            string error_msg = "操作失败";
            int error_code = (int)ReturnCode.OPERATION_FAILED;
            UserViewModel model = new UserViewModel();
            model.LoginId = query.LoginID;
            model.UserName = query.UserName;           
            model.Gender = query.Gender;
            model.RoleID = query.RoleID;
            model.UserPhone = query.UserPhone;           
            switch (query.Flag)
            {
                case 1://添加
                    model.UserPwd = query.UserPwd;
                    model.Creator = query.Creator;
                    service.AddUser(model, out error_msg, out error_code);
                    return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0,0,0, error_msg, error_code, null));
                case 2://修改
                    model.UserID = query.UserID;
                    model.UserState = query.UserState;
                    model.Updator = query.Updator;
                    var listU = service.UpdateUser(model,out error_msg,out error_code);
                    return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0, 0, 0, error_msg, error_code, null)); 
            }            
            return Ok(error_msg);
        }
        
        [HttpPost]
        [Route("api/User/UpateState")]
        public IHttpActionResult UpateState([FromBody]dynamic query)
        {
            string error_msg = "操作失败";
            int error_code = (int)ReturnCode.OPERATION_FAILED;
            if(query.UserID ==""|| string.IsNullOrEmpty(query))
            {
                error_msg = "信息有误，请重新输入";
            }else
            {
                service.UpdateUserState(query.UserID, query.UserState, out error_msg, out error_code);
            }
           
            return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0, 0, 0, error_msg, error_code, null));
        }
    }
}
