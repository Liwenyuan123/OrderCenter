using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.ViewEnum
{
    public enum UserRole
    {
        [Description("前端")]
        FRONT_END = 1,
        [Description("后端")]
        BACK_END = 2
    }
}
