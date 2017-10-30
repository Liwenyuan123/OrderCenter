using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    public class LoginDataModel
    {
        public Guid Uid { get; set; }
        public int AppKey { get; set; }
        public string Token { get; set; }
        public UserInfoSelfViewModel userInfoSelfViewModel { get; set; }

    }
}
