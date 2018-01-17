using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.CommHelper
{
    public  class Return_ResultJsonModel<T>where T:class
    {
        public int pageIndex { get; set; }
        public int pageCount { get; set; }
        public int pageTotal { get; set; }
        public string error_msg { get; set; }
        public int error_code { get; set; }
        public List<T> data { get; set; }

        public   Return_ResultJsonModel(int pageIndex,int pageCount,int pageTotal, string msg,int code,List<T> data)
        {
            this.pageIndex = pageIndex;
            this.pageCount = pageCount;
            this.pageTotal = pageTotal;
            this.error_msg = msg;
            this.error_code = code;
            this.data = data;
        }
    }
    
    
}
