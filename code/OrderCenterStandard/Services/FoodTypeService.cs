using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderCenterStandard.Services
{
    public class FoodTypeService
    {
        public IQueryable<O_FoodType> GetAll()
        {
            var db = new OrderCentEntities();
            var models = from m in db.O_FoodType where m.State == 1 && m.ID > 0 select m;
            return models;
        }
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool AddFoodType(List<O_FoodType> models)
        {
            var result = false;
            O_FoodType item3 = new O_FoodType();
            using (var db = new OrderCentEntities())
            {
                foreach (var model in models)
                {
                    O_FoodType item = new O_FoodType();
                    item.PID = 0;
                    item.State = 1;
                    item.TypeName = model.TypeName;
                    item.Remark = model.Remark;
                    db.O_FoodType.Add(item);
                }
                result = db.SaveChanges() > 0;
            }
            return result;
        }

    }
}