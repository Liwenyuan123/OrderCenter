using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderCenterStandard.ViewModels
{
    public class OrderView
    {
        public string MainID { get; set; }
       // public string OrderNum { get; set; }
        public string UsePersonName { get; set; }
        public string Phone { get; set; }
        //public string ReceivePerson { get; set; }
       // public Nullable<int> State { get; set; }
        public string Remark { get; set; }
        //public Nullable<int> OrderState { get; set; }
        //public DateTime AddDate { get; set; }
        public List<O_OrderDetail> OrderDetail { get; set; }
    }
}