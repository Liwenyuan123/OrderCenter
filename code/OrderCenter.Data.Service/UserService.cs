﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;
using System.Linq.Expressions;

namespace OrderCenter.Data.Service
{
    public class UserService
    {
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




        //add userInfo
        public LoginDataModel addUser(string loginID, string pwd, string phone)
        {
            using (var db = new OrderCentDB())
            {
                O_UserInfo model = new O_UserInfo();
                model.UID = Guid.NewGuid();
                model.Phone = phone;
                model.PassWord = pwd;
                model.State = (int)RecordState.NORMAL;
                db.O_UserInfo.Add(model);
                LoginDataModel loginModel = new LoginDataModel();
                loginModel.Uid = model.UID;
                loginModel.Token = "";
                loginModel.AppKey = 1;
                loginModel.userInfoSelfViewModel.LoginId = model.UserName;
                
                loginModel.userInfoSelfViewModel.UserUid = model.UID.ToString();
                return loginModel;
            }
        }

        public List<UserManageViewModel> SelectUsers(string loginId, int userState, int pageIndex, out int pageTotal, out int pageCount)
        {
            Expression<Func<O_UserInfo, bool>> where = c => true;
            if (!string.IsNullOrEmpty(loginId))
            {
                where = where.And(c => c.UserName.Contains(loginId));
            }
            if (userState == 0 || userState == 1)
            {
                where = where.And(c => c.State == userState);
            }

            using (var db = new OrderCentDB())
            {
                //总条数
                pageTotal = db.O_UserInfo.Where(where).Count();
                //总页数
                pageCount = Convert.ToInt32(Math.Ceiling((decimal)pageTotal / PageSize.Count));
                var list = db.O_UserInfo.Where(where).Skip((pageIndex - 1) * PageSize.Count).Take(PageSize.Count).Select(t => new UserManageViewModel { Uid = t.UID.ToString(), LoginID = t.UserName, Phone = t.Phone, State = Enum.GetName(typeof(OrderState), Convert.ToInt32(t.State)) }).ToList();
                return list;

            }
        }

        //delete user
        public bool deleteUser(string uid)
        {
            using (var db = new OrderCentDB())
            {
                var model = db.O_UserInfo.FirstOrDefault(c => c.UID.ToString() == uid);
                model.State = (int)RecordState.DELETE;
                return db.SaveChanges() > 0;
            }
        }
        public bool updateUser(string uid,string loginId,string userName,string phone)
        {
            using (var db = new OrderCentDB())
            {
                var model = db.O_UserInfo.FirstOrDefault(c => c.UID.ToString() == uid);
                model.LoginID = loginId;
                model.UserName = userName;
                model.Phone = phone;
                return db.SaveChanges() > 0;
            }
        }
    }
}
