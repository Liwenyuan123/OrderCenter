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
        public string UserID { get; set; }
        // 登录名
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        

      
       
    }
}
