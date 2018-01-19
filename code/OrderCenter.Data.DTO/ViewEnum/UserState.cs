using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.ViewEnum
{
    public enum UserState
    {
        [Description("正常")]
        NORMAL = 0,
        [Description("停用")]
        DISABLE = 1,
        [Description("删除")]
        DELETED = 2

    }
}
