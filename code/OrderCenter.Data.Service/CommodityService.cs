using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCenter.Data.DTO;

namespace OrderCenter.Data.Service
{
   public class CommodityService
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommodityViewModel> GetAll()
        {

            using (var db = new OrderCentDB())
            {
                var lists = from c in db.O_CommodityInfo join b in db.O_FoodType on c.TypeID equals b.ID where c.State == 1 select new CommodityViewModel() { UID = c.UID, ComName = c.ComName, Standard = c.Standard, Unit = c.Unit, Price = c.Price??0, PriceSum = c.PriceSum??0, TypeID = b.ID};
                return lists;
            }


        }

        public bool Add(CommodityViewModel model)
        {
            using (var db = new OrderCentDB())
            {
                O_CommodityInfo newModel = new O_CommodityInfo();
                newModel.UID = Guid.NewGuid();
                newModel.ComName = model.ComName;
                newModel.Price = model.Price;
                newModel.PriceSum = model.PriceSum;
                //newModel.Remark = model.Remark;
                newModel.Standard = model.Standard;
                newModel.State = 1;
                newModel.TypeID = model.TypeID;
                newModel.Unit = model.Unit;
                db.O_CommodityInfo.Add(newModel);
                return db.SaveChanges() > 0;
            }
        }
        //修改
        public bool Update(CommodityViewModel model)
        {
            using (var db = new OrderCentDB())
            {
                O_CommodityInfo oldModel = db.O_CommodityInfo.FirstOrDefault(c => c.UID == model.UID);
                //oldModel.UID = model.UID;
                oldModel.Unit = model.Unit;
                oldModel.TypeID = model.TypeID;
                //oldModel.State = model.State;
                oldModel.Standard = model.Standard;
                //oldModel.Remark = model.Remark;
                oldModel.PriceSum = model.PriceSum;
                oldModel.Price = model.Price;
                oldModel.ComName = model.ComName;

                return db.SaveChanges() > 0;
            }
        }
    }

}
