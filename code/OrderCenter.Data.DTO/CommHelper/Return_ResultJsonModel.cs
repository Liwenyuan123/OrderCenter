using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.CommHelper
{
    public  class Return_ResultJsonModel<T>where T:class
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int PageTotal { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
        public List<T> Models { get; set; }

        public   Return_ResultJsonModel(int pageIndex,int pageCount,int pageTotal, string msg,int code,List<T> data)
        {
            this.PageIndex = pageIndex;
            this.PageCount = pageCount;
            this.PageTotal = pageTotal;
            this.Msg = msg;
            this.Code = code;
            this.Models = data;
        }
    }
    
    
}
