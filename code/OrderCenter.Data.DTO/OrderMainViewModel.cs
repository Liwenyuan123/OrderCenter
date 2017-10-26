using OrderCenter.Data.DTO.ViewEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    public class OrderMainViewModel
    {
        //主单ID,主键
        public string MainID { get; set; }
        //订单编号
        public string OrderNum { get; set; }
        //下单人
        public string UsePersonName { get; set; }
        //下单人电话
        public string Phone { get; set; }
        
        public string Address { get; set; }
        //订单状态
        public  OrderState OrState { get; set; }
        //public DateTime AddDate { get; set; }
        public List<OrderDetailViewModel> OrderDetail { get; set; }
    }
}
