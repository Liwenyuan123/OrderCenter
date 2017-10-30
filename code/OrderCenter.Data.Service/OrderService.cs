using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;

namespace OrderCenter.Data.Service
{
    public class OrderService
    {

        public virtual List<dynamic> GetPageDate<T, TKey>(Expression<Func<T, dynamic>> select, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int Total)
            where T : class
        {
            OrderCentDB db = new OrderCentDB();
            Total = db.Set<T>().Where(where).Count();
            var list = db.Set<T>().Where(where).OrderByDescending(order).Select(select).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return list.ToList();
        }

        public List<dynamic> GetOrderMainList(out int totalNum)
        {
            var lists = GetPageDate<O_OrderMain, DateTime>(c => new { c.OrderNum, c.OrderState, c.Phone, c.ReceivePerson, c.UID, c.UsePersonName, c.Remark }, c => c.State == 1, c => c.AddDate, 2, 4, out totalNum);
            return lists;
        }
        public List<OrderMainViewModel> Select(string startDate,string endDate,int orderState, int pageIndex,out int pageCount,out int pageTotal)
        {
            Expression<Func<O_OrderMain, bool>> where = t => true;
            if (!string.IsNullOrEmpty(startDate))
            {
               where = where.And(t => t.AddDate >=Convert.ToDateTime( startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                where = where.And(t => t.AddDate <= Convert.ToDateTime(endDate));
            }
            if(orderState != 0)
            {
                where = where.And(t => t.OrderState == orderState);
            }
            using (var db = new OrderCentDB())
            {
                //总条数
                pageTotal = db.O_OrderMain.Where(where).Count();
                //总页数
                pageCount =Convert.ToInt32( Math.Ceiling((decimal) pageTotal / PageSize.Count));
                var list = db.O_OrderMain.Where(where).Skip((pageIndex - 1) * PageSize.Count).Take(PageSize.Count).Select(t=>new OrderMainViewModel{ MainID =t.UID.ToString(), OrderNum =t.OrderNum, UsePersonName =t.UsePersonName, Phone =t.Phone, Address =t.Address, OrState =Enum.GetName(typeof( OrderState),Convert.ToInt32( t.OrderState) )}).ToList();
                return list;
               
            } 
        }
        
        /// <summary>
        /// 根据MainID 获取明细信息
        /// </summary>
        /// <param name="mainId"></param>
        /// <returns></returns>
        public List<O_OrderDetail> GetOrderDetail(string mainId)
        {
            using (var db = new OrderCentDB())
            {
                var models = from m in db.O_OrderDetail where m.MainId == mainId && m.State == 1 select m;
                return models.ToList();
            }
        }

        public bool AddOrder(O_OrderMain orderMain, List<O_OrderDetail> orderDetails)
        {
            using (var db = new OrderCentDB())
            {
                //添加主表信息
                using (var tran = db.Database.BeginTransaction())
                {
                    orderMain.UID = Guid.NewGuid();
                    orderMain.OrderNum = CreatOrderNum();
                    db.O_OrderMain.Add(orderMain);
                    db.SaveChanges();
                    //添加明细信息
                    foreach (O_OrderDetail d in orderDetails)
                    {
                        d.UID = Guid.NewGuid();
                        d.MainId = orderMain.UID.ToString();
                        d.State = 1;
                        db.O_OrderDetail.Add(d);
                    }
                    db.SaveChanges();
                    tran.Commit();
                }

            }
            return true;
        }

        public bool UpdateOrderState(string mainId,int orderState)
        {
            bool re = false;
            
            using (var db = new OrderCentDB())
            {
                var model = db.O_OrderMain.FirstOrDefault(c => c.UID.ToString() == mainId);
                model.OrderState = orderState;
                if (db.SaveChanges() > 0) { re = true; }

                return re;
            }
        }
        
        public string CreatOrderNum()
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd");
            str += Guid.NewGuid().ToString();
            return str;
        }

    }

}
