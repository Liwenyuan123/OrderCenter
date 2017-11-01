using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.ViewEnum
{
    public enum RecordState
    {
        [Description("启用")]
        NORMAL = 1,
        [Description("禁用")]
        DELETE = 0
    }
}
