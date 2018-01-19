using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
   public  class CompanyApplyViewModel
    {
        public string ID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
       // public string CompanyImage { get; set; }
        public int CompanyType { get; set; }
        public int ApplyType { get; set; }
        public int AccountCount { get; set; }
        public int UseTime { get; set; }
        public string LinkMan { get; set; }
        public string LinkPhone { get; set; }
        public string Tel { get; set; }
        public decimal TotalBalance { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public int IsPaid { get; set; }
        public Nullable<int> PayWay { get; set; }
        public string PayAccount { get; set; }
        public string PayCode { get; set; }
        public int ApplyState { get; set; }
        public string Creator { get; set; }
    }
}
