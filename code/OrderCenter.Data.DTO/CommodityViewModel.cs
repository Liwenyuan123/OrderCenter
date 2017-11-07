using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
    public class CommodityViewModel
    {
        //商品主键
        public Guid? UID { get; set; }
        //商品名称
        public string ComName { get; set; }
        //规格
        public string Standard { get; set; }
        //单位
        public string Unit { get; set; }
        //单价
        public decimal Price { get; set; }
        //总价（整箱价格）
        public decimal PriceSum { get; set; }
        //类别ID
        public int TypeID { get; set; }
        //类别名称
        public string TypeName { get; set; }
    }
}
