using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO;

namespace OrderCenter.Data.Service
{
   public class FoodTypeService
    {
        public List<FoodTypeViewModel> GetAll()
        {
            using (var db = new OrderCentDB())
            {
                var models = db.O_FoodType.Where(m => m.State == (int)RecordState.NORMAL).Select(c=>new FoodTypeViewModel() { ID = c.ID,TypeName = c.TypeName}).ToList();
                return models;
            }
        }
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool Add(List<O_FoodType> models)
        {
            var result = false;
            O_FoodType item3 = new O_FoodType();
            using (var db = new OrderCentDB())
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
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = new OrderCentDB())
            {
                O_FoodType model = db.O_FoodType.FirstOrDefault(c => c.ID == id);
                model.State = 0;
                return db.SaveChanges() > 0;

            }
        }

    }
}
