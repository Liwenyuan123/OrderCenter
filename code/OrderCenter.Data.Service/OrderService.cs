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
        
        //get orderDetailList by MainID
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

        //get orderMain by UserID
        public List<O_OrderMain> GetOrderByUserID(string userId)
        {
            using (var db = new OrderCentDB())
            {
                var models = db.O_OrderMain.Where(c => c.State == 1 && c.UserID == userId).ToList();
                return models;
            }
            
        }

        /// <summary>
        /// Login Service
        /// </summary>
        /// <param name="loginId">loginId</param>
        /// <param name="random">random</param>
        /// <param name="timeStamp">timeStamp</param>
        /// <param name="secretString">secretString</param>
        /// <returns></returns>
        public LoginDataModel UserLogin(string loginId, int random, long timeStamp, string secretString)
        {
            //timeStamp verification timespan like:1505443441000
            //bool isTimeStampValid = (DateTime_Helper_DG.GetCurrentTimeStamp() - timeStamp / 1000) <= QX_Frame_Data_Config.RequestExpireTime * 60;
            //if (!isTimeStampValid)
            //{
            //    throw new Exception_DG_Internationalization(3009);
            //}

            //[random+timestamp] can be find in cache? -- multiple request refuse
            //if (Cache_Helper_DG.Cache_Get($"{random}{timeStamp}".ToHashString()) != null)
            //{
            //    throw new Exception_DG_Internationalization(3010);
            //}
            //Cache_Helper_DG.Cache_Add($"{random}{timeStamp}".ToHashString(), 1);//add [random+timestamp] into cache

            //get MD5[pwd] from database
            //UserAccount userAccount = QuerySingle(new UserAccountQueryObject { QueryCondition = t => t.LoginId.Equals(loginId) || t.Tel.Equals(loginId) || t.Email.Equals(loginId) }).Cast<UserAccount>();

            //if (userAccount == null)
            //{
            //    throw new Exception_DG_Internationalization(3011);
            //}

            //MD5[loginid+MD5[pwd]+ramdom+timestamp]==MD5[secretMessage]?
            //hex_md5(hex_md5(loginId + hex_md5(pwd) + random + timeStamp) + loginId + loginId)
            //bool secretStringMatched = MD5_Encrypt(MD5_Encrypt($"{loginId}{userAccount.Password}{random}{timeStamp}") + "qx_frame" + loginId).Equals(secretString);
            //if (!secretStringMatched)
            //{
            //    //account or pwd error
            //    throw new Exception_DG_Internationalization(3012);
            //}

            //get rsa keys
            //RSA_Keys rsa_Keys = RSA_GetKeys();

            //TokenAuthentication authentication = QuerySingle(new TokenAuthenticationQueryObject { QueryCondition = t => t.UserUid.Equals(userAccount.UserUid) }).Cast<TokenAuthentication>();

            //if (authentication == null)
            //{
            //    authentication = TokenAuthentication.Build();
            //    //secretKey=MD5[UserUid+timeStamp+random]
            //    authentication.TokenSign = MD5_Encrypt($"{userAccount.UserUid}{timeStamp}{random}");
            //    authentication.RSA_PublicKey = rsa_Keys.PublicKey;
            //    authentication.RSA_PrivateKey = rsa_Keys.PrivateKey;
            //    authentication.UserUid = userAccount.UserUid;
            //    if (!authentication.Add(authentication))
            //    {
            //        throw new Exception_DG_Internationalization(3013);
            //    }
            //    authentication = QuerySingle(new TokenAuthenticationQueryObject { QueryCondition = t => t.UserUid.Equals(userAccount.UserUid) }).Cast<TokenAuthentication>();
            //}
            //else
            //{
            //    //secretKey=MD5[UserUid+timeStamp+random]
            //    authentication.TokenSign = MD5_Encrypt($"{userAccount.UserUid}{timeStamp}{random}");
            //    authentication.RSA_PublicKey = rsa_Keys.PublicKey;
            //    authentication.RSA_PrivateKey = rsa_Keys.PrivateKey;
            //    authentication.UserUid = userAccount.UserUid;
            //    if (!authentication.Update(authentication))
            //    {
            //        throw new Exception_DG_Internationalization(3013);
            //    }
            //}

            //long expireTimeStamp = DateTime_Helper_DG.GetTimeStampByDateTimeUtc(DateTime.UtcNow.AddDays(QX_Frame_Data_Config.AuthTokenExpireTime_days).AddHours(QX_Frame_Data_Config.AuthTokenExpireTime_hours).AddMinutes(QX_Frame_Data_Config.AuthTokenExpireTime_minutes));

            ////token=RSA_publicKey[uid+loginid+expiretimestamp+tokensign]
            //string token = RSA_Encrypt($"{userAccount.UserUid}&{expireTimeStamp}&{authentication.TokenSign}", rsa_Keys.PublicKey);

            LoginDataModel loginDataModel = new LoginDataModel();
            //loginDataModel.Uid = userAccount.UserUid;
            //loginDataModel.AppKey = authentication.AppKey;
            //loginDataModel.Token = token;
            //loginDataModel.userInfoSelfViewModel = GetUserInfoViewModelSelfByUid(userAccount.UserUid);

            ////record login history
            //LoginHistory loginHistory = new LoginHistory
            //{
            //    UserUid = userAccount.UserUid,
            //    LoginIp = Ip_Helper_DG.GetServerIpAddress()
            //};

            //if (!loginHistory.Add(loginHistory))
            //{
            //    throw new Exception_DG_Internationalization(3020);
            //}

            return loginDataModel;
        }
    }

}
