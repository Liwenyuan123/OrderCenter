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
    public class UserManageController : ApiController
    {
        UserService service = new UserService();
       
        //GET:api/UserManage
        public IHttpActionResult Post(string LoginId,int State,int PageIndex)
        {
            int pageCount = 0;
            int pageTotal = 0;
            string Msg = "操作失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            var models = service.SelectUsers(LoginId, State, PageIndex, out pageCount, out pageTotal,out Msg,out Code);
            
            return Json(new Return_ResultJsonModel<UserManageViewModel>(PageIndex,pageCount,pageTotal,Msg,Code,models));
           
        }
        //POST:api/UserMage
        public IHttpActionResult Post([FromBody]dynamic query, string Flag)
        {
            string Msg = "操作失败";
            int Code = (int)ReturnCode.OPERATION_FAILED;
            switch (Flag)
            {
                case "删除":
                    var list = service.deleteUser(query.Uid, out Msg, out Code);
                    return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0,0,0,Msg,Code,list));
                case "修改":
                    var listU = service.updateUser(query.Uid,query.LoginId,query.UserName,query.Phone,out Msg,out Code);
                    return Json(new Return_ResultJsonModel<UserInfoSelfViewModel>(0, 0, 0, Msg, Code, listU));
                    break;
                case "修改密码":
                    service.updatePassWord(query.Uid, query.PassWord, out Msg);
                    break;

            }
            
            return Ok(Msg);
        }
    }
}
