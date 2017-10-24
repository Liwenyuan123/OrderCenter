using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderCenterStandard.ViewModels
{
    public class OrderView
    {
        public O_OrderMain OrderMain { get; set; }
        public List<O_OrderDetail> OrderDetail { get; set; }
    }
}