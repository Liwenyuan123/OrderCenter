using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    /// <summary>
    /// self user info ,others can not be get
    /// </summary>
    public class UserInfoSelfViewModel
    {
        // PK（identity）  User UID
        public String UserUid { get; set; }
        // 登录名
        public String LoginId { get; set; }
        public String Pwd { get; set; }
        // 电话号码
        public String Tel { get; set; }

      
       
    }
}
