using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenter.Data.Service;
using OrderCenter.Data.DTO;

namespace OrderCenterStandard.Controllers
{
    public class UserManageController : ApiController
    {
        UserService service = new UserService();
        // POST: api/UserManage
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //check args
            //SF_Frame_Valudation.CheckDataNullValudation.CheckNullGet(query, "数据为空");
            //string loginId = query.loginId;
            //SF_Frame_Valudation.CheckFormatValidation.CheckMobilePhone(query.Tel, "电话号码数据不正确");
            //string phone = query.Tel;
            //string pwd = query.Pwd;

            ////support loginid,phone,email login!
            //LoginDataModel loginDataModel = service.addUser(loginId, pwd, phone);
            //return Ok(loginDataModel);
            return Ok();
        }
        //GET:api/UserManage
        public IHttpActionResult Post(string LoginId,int State,int PageIndex)
        {
            int pageCount = 0;
            int pageTotal = 0;
            var models = service.SelectUsers(LoginId, State, PageIndex, out pageCount, out pageTotal);
            
            var data = new { PageIndex = PageIndex, PageCount = pageCount, PageTotal = pageTotal, Data = models };
            return Json(data);
           
        }
        //POST:api/UserMage
        public IHttpActionResult Post([FromBody]dynamic query, string Flag)
        {
            string Msg = "操作失败";
            switch (Flag)
            {
                case "删除":
                    if (service.deleteUser(query.Uid)) Msg = "操作成功";
                    break;
                case "修改":
                    if (service.updateUser(query.Uid,query.LoginId,query.UserName,query.Phone)) Msg = "操作成功";
                    break;
                case "修改密码":
                    service.updatePassWord(query.Uid, query.PassWord, out Msg);break;

            }
            
            return Ok(msg);
        }
    }
}
