using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    public class UserViewModel
    {
       
        // 登录名
        public string UserID { get; set; }
        public string LoginId { get; set; }
        public string UserPwd { get; set; }
        public string UserName { get; set; }
        //性别
        public int Gender { get; set; }
        //角色
        public string RoleID { get; set; }
        public string UserPhone { get; set; }
        public int UserState { get; set; }
        public string Creator { get; set; }
        public string Updator { get; set; }
    }
}
