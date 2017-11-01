using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.DTO
{
   public class FoodTypeViewModel
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public virtual List<CommodityViewModel> CommodityViewModels { get; set; }
    }
}
