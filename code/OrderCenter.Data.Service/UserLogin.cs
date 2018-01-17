using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO.CommHelper;
using System.Linq.Expressions;

namespace OrderCenter.Data.Service
{
    public class UserLogin
    {
        public UserInfoSelfViewModel app_UserLogin(string loginID, string loginPwd, out string error_message, out int error_code)
        {
           
            using (var db = new PeiSongEntities())
            {
                var userInfo = db.S_User.FirstOrDefault(c => c.LogCode == loginID);
                UserInfoSelfViewModel model = new UserInfoSelfViewModel();
                if (userInfo == null) { error_message = "用户不存在";error_code = -1;return model; }
                string pwd = Encrypt_Helper_SF.UserMd5(loginPwd + "SF_Frame_app_8");
                if (userInfo.UserPwd != pwd) { error_message = "账号或密码错误";error_code = -1;return model; }
                error_message = "登录成功";
                error_code = (int)ReturnCode.OK;
                
                model.UserID = userInfo.UserID.ToString();
                model.LoginId = userInfo.LogCode;
                model.Phone = userInfo.UserPhone;
                model.UserName = userInfo.UserName;
                return model;
            }

        }
       
       
            





}
}
