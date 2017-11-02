using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO.CommHelper
{
    public static class Return_ResultJsonModel
    {
        public static JsonModel CreateResultJson(string msg,int code,List<T> data)
        {

        }
    }
    public static class JsonModel
    {
        public static string Msg { get; set; }
        public static int Code { get; set; }
        public static List<T> Data<T> { get;set;}
    }
}
