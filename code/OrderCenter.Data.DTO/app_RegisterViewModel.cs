using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    public class app_RegisterViewModel
    {
       
        // 登录名
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PassWord { get; set; }
    }
}
