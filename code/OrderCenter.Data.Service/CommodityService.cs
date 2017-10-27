using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCenter.Data.DTO;
using System.Linq.Expressions;

namespace OrderCenter.Data.Service
{
   public class CommodityService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="ComName">商品名称</param>
        /// <param name="TypeID">商品类别</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="PageTotal">总条数</param>
        /// <param name="PageSize">每页条数</param>
        /// <returns></returns>
        public List<CommodityViewModel> Select(string ComName,int  TypeID, int PageIndex, int PageCount, out int PageTotal,int PageSize)
        {
            PageTotal = 0;
            Expression<Func<O_CommodityInfo, bool>> where = t => true;
            if (!string.IsNullOrEmpty(ComName))
            {
               where = PredicateExtensions.And<O_CommodityInfo>(where,t=> t.ComName.Contains(ComName));
            }
            if(TypeID != 0 )
            {
                where = PredicateExtensions.And<O_CommodityInfo>(where, t => t.TypeID == TypeID);
            }
            
            using (var db = new OrderCentDB())
            {
                var models = db.O_CommodityInfo.Where(where).Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(c=>new CommodityViewModel() { UID =c.UID,ComName =c.ComName,Standard = c.Standard,Unit = c.Unit,Price = c.Price??0,PriceSum = c.PriceSum??0,TypeName = c.O_FoodType.TypeName}).ToList();
                PageTotal = db.O_CommodityInfo.Where(where).Count();

                return models;
            }


        }
       public CommodityViewModel GetByID(string uid)
        {
            using (var db = new OrderCentDB())
            {
                var model = db.O_CommodityInfo.Where(c=>c.UID.ToString() == uid).Select(c => new CommodityViewModel() { UID = c.UID, ComName = c.ComName, Standard = c.Standard, Unit = c.Unit, Price = c.Price ?? 0, PriceSum = c.PriceSum ?? 0, TypeName = c.O_FoodType.TypeName }).ToList();
                return model[0];
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
        //删除
        public bool Delete(string uid)
        {
            using (var db = new OrderCentDB())
            {
                O_CommodityInfo oldModel = db.O_CommodityInfo.FirstOrDefault(c => c.UID.ToString() == uid);
                if (oldModel == null) return false;
                oldModel.State = 0;
                return db.SaveChanges() > 0;
            }

        }
    }
   

}
