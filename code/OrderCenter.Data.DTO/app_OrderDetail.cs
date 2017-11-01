using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    public class app_OrderDetail
    {
        public System.Guid ComUID { get; set; }
        //商品名
        public string ComName { get; set; }
        //规格
        public string Standard { get; set; }
        //单位
        public string Unit { get; set; }
        //计划金额
        public decimal PricePlan { get; set; }
        //计划总金额
        public decimal PriceSumPlan { get; set; }
        //订购数量
        public double NumPlan { get; set; }
        
    }
}
