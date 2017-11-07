using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderCenterStandard.Models
{
    public class ReturnOK
    {
        public static object  OK(int pageIndex, int pageCount, int pageTotal, string msg, int code, Object data)
        {
            return new{ PageIndex = pageIndex, PageCount = pageCount, PageTotal = pageTotal, Msg = msg, Code = code, Models = data };
        }
        
    }
}