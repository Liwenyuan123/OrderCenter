using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;


namespace OrderCenterStandard.Services
{
    public class OrderService
    {
       
        //使用rptCate.DataSource = getPageDate<Category, int>(c => new { c.Name, c.CreatedDate, c.CreatedBy }, c => c.Id > 0, c => c.Id, 2, 4, out Total_);
        //rptCate.DataBind();
        public List<dynamic> GetPageDate<T, TKey>(Expression<Func<T, dynamic>> select, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int Total)
            where T : class
        {
            OrderCentEntities db = new OrderCentEntities();
            Total = db.Set<T>().Where(where).Count();
            var list = db.Set<T>().Where(where).OrderByDescending(order).Select(select).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return list.ToList();
        }

        public List<dynamic> GetOrderMainList(out int totalNum)
        {
            var lists = GetPageDate<O_OrderMain, DateTime>(c => new { c.OrderNum, c.OrderState, c.Phone, c.ReceivePerson, c.UID, c.UsePersonName, c.Remark }, c => c.State == 1, c => c.AddDate, 2, 4, out totalNum);
            return lists;
        }

        /// <summary>
        /// 根据MainID 获取明细信息
        /// </summary>
        /// <param name="mainId"></param>
        /// <returns></returns>
        public List<O_OrderDetail> GetOrderDetail(string mainId)
        {
            using (var db = new OrderCentEntities())
            {
                var models = from m in db.O_OrderDetail where m.MainId == mainId && m.State == 1 select m;
                return models.ToList();
            }
        }
        public bool AddOrder(O_OrderMain orderMain,List<O_OrderDetail> orderDetails)
        {
            using (var db = new OrderCentEntities())
            {
                //添加主表信息
                using (var tran = db.Database.BeginTransaction())
                {
                    orderMain.UID = Guid.NewGuid();
                    orderMain.OrderNum = CreatOrderNum();
                    db.O_OrderMain.Add(orderMain);
                    db.SaveChanges();
                    //添加明细信息
                    foreach(O_OrderDetail d in orderDetails)
                    {
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
        public string CreatOrderNum()
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd");
            str += Guid.NewGuid().ToString();
            return str;
        }
        
    }
}