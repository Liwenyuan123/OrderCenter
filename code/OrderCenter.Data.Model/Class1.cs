using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Data.Model
{
    class Class1
    {
        public void getModel()
        {
            using(var db = new db_OrderCentEntities())
            {
                var list = db.O_CommodityInfo.Select(c=> new{ CommName = c.ComName,TypeName = c.O_FoodType.TypeName});
            }
        }
    }
}
