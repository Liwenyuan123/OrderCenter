//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderCenter.Data.Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class O_OrderDetail
    {
        public System.Guid UID { get; set; }
        public string MainId { get; set; }
        public string CommodityId { get; set; }
        public string ComName { get; set; }
        public string Standard { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> PricePlan { get; set; }
        public Nullable<double> NumPlan { get; set; }
        public Nullable<decimal> PriceSumPlan { get; set; }
        public Nullable<double> NumReal { get; set; }
        public Nullable<decimal> PriceReal { get; set; }
        public Nullable<decimal> PriceSumReal { get; set; }
        public Nullable<int> OrderState { get; set; }
        public Nullable<int> State { get; set; }
    }
}