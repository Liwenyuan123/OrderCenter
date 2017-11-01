using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.ViewEnum
{
    public enum OrderState
    {
        [Description("待审核")]
        normal = 1,
        [Description("审核通过")]
        AuditPassed = 2,
        [Description("审核不通过")]
        AuditNo = 3,

       [Description("发货")]
        Deliver =5,
        [Description("已完成")]
        IsOver = 4
    }
}
