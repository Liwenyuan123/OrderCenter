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
    
    public partial class S_TimeApply
    {
        public string ApplyCode { get; set; }
        public string CompanyCode { get; set; }
        public System.DateTime ApplyTime { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal Discount { get; set; }
        public Nullable<int> ApplyState { get; set; }
        public int IsPaid { get; set; }
        public Nullable<int> PayWay { get; set; }
        public string PayAccount { get; set; }
        public string PayCode { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Updator { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
