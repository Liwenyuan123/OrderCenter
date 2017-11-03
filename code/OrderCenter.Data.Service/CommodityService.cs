using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCenter.Data.DTO;
using System.Linq.Expressions;
using OrderCenter.Data.DTO.ViewEnum;

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
        public List<CommodityViewModel> Select(string ComName, int TypeID, int PageIndex,out int PageCount, out int PageTotal, int PageSize,out int Code,out string Msg)
        {
            Msg = "操作失败";
            Expression<Func<O_CommodityInfo, bool>> where = t => true;
            if (!string.IsNullOrEmpty(ComName) && ComName != "0")
            {
                where = PredicateExtensions.And<O_CommodityInfo>(where, t => t.ComName.Contains(ComName));
            }
            if (TypeID != 0)
            {
                where = PredicateExtensions.And<O_CommodityInfo>(where, t => t.TypeID == TypeID);
            }

            using (var db = new OrderCentDB())
            {
                var models = db.O_CommodityInfo.Where(where).OrderBy(c=>c.ComName).Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(c => new CommodityViewModel() { UID = c.UID, ComName = c.ComName, Standard = c.Standard, Unit = c.Unit, Price = c.Price ?? 0, PriceSum = c.PriceSum ?? 0, TypeName = c.O_FoodType.TypeName });

                PageTotal = db.O_CommodityInfo.Where(where).Select(m => m.UID).AsQueryable().Count();
                PageCount =Convert.ToInt32( Math.Ceiling(Convert.ToDecimal(PageTotal) / PageSize));
                Msg = "查询成功";
                Code = (int)ReturnCode.OK;
                return models.ToList();
            }


        }
        public CommodityViewModel GetByID(string uid)
        {
            using (var db = new OrderCentDB())
            {
                var model = db.O_CommodityInfo.Where(c => c.UID.ToString() == uid).Select(c => new CommodityViewModel() { UID = c.UID, ComName = c.ComName, Standard = c.Standard, Unit = c.Unit, Price = c.Price ?? 0, PriceSum = c.PriceSum ?? 0, TypeName = c.O_FoodType.TypeName }).ToList();
                return model[0];
            }
        }

        public bool Add(CommodityViewModel model,out string Msg,out int Code)
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
                bool re = db.SaveChanges() > 0;
                if (re)
                {
                    Msg = "操作成功";
                    Code = (int)ReturnCode.OK;
                }
                else { re = false;
                    Msg = "操作失败";
                    Code = (int)ReturnCode.OPERATION_FAILED;
                }
                
                return re ;
            }
        }
        //修改
        public bool Update(CommodityViewModel model,out string Msg,out int Code)
        {
            Msg = "操作失败"; Code = (int)ReturnCode.OPERATION_FAILED;
            using (var db = new OrderCentDB())
            {
                O_CommodityInfo oldModel = db.O_CommodityInfo.FirstOrDefault(c => c.UID == model.UID);
                oldModel.Unit = model.Unit;
                oldModel.TypeID = model.TypeID;
                oldModel.Standard = model.Standard;
                oldModel.PriceSum = model.PriceSum;
                oldModel.Price = model.Price;
                oldModel.ComName = model.ComName;
                bool re = db.SaveChanges() > 0;
                if (re)
                {
                    Msg = "操作成功";Code = (int)ReturnCode.OK;
                }
                return re;
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

        public dynamic GroupByType()
        {
            using (var db = new  OrderCentDB())
            {

                //var result = from o in db.O_CommodityInfo join m in db.O_FoodType on o.TypeID equals m.ID into om from omD in om.DefaultIfEmpty() group omD  by o.TypeID into g select new { TypeId = g.Key,TypeName = g.TypeName ,CommodityList = g };
                var result = from o in db.O_CommodityInfo join m in db.O_FoodType on o.TypeID equals m.ID where o.State == (int)RecordState.NORMAL select new { UID = o.UID, ComName = o.ComName, Price = o.Price, PriceSum = o.PriceSum, TypeID = o.TypeID, TypeName = m.TypeName, Standard = o.Standard };
                var re = from o in result group o by o.TypeName  into g select new { TypeName = g.Key, ComList = g };
                return re.ToList();

            }
        }
    }


}
