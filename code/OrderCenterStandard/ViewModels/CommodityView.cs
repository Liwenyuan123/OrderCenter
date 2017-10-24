using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderCenterStandard.ViewModels
{
    public class CommodityView
    {
        
        public Guid UID { get; set; }
        public string ComName { get; set; }
        public string Standard { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PriceSum { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string Remark { get; set; }
        //类别信息
        public Nullable<int> PID { get; set; }
        public string TypeName { get; set; }
    }
}