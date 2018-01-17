using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCenter.Data.DTO;
using OrderCenter.Data.DTO.ViewEnum;
using OrderCenter.Data.DTO.CommHelper;
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
        public LoginDataModel UserLogin(string loginId,  string secretString)
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
        public UserInfoSelfViewModel app_RegisterUser(app_RegisterViewModel modelView,out string Msg,out int Code)
        {
            using (var db = new PeiSongEntities())
            {
                Msg = "操作失败";
                P_Person model = new P_Person();
                model.PersonID = Guid.NewGuid().ToString("N");
                model.LogID = modelView.LoginId;
                model.PersonName = modelView.UserName;
                model.PersonPhone = modelView.Phone;
                model.PersonPwd = Encrypt_Helper_SF.UserMd5(modelView.PassWord.Trim()+"SF_Frame_app_8");
                model.CompanyCode = "";// modelView.Address;
                model.RoleType = 0;// (int)RecordState.NORMAL;
                model.AccountBegin = DateTime.Now;
                model.AccountEnd = DateTime.Now.AddDays(15);
                db.P_Person.Add(model);
                if (db.SaveChanges() > 0) Msg = "注册成功";
                UserInfoSelfViewModel reModel = new UserInfoSelfViewModel();
                reModel.UserID = model.PersonID.ToString();
                reModel.UserName = model.PersonName;
                reModel.LoginId = model.LogID;
                reModel.Phone = model.PersonPhone;
                //reModel.Address = model.Address;
                Code = (int)ReturnCode.OK;
                return reModel;
            }
        }

        public UserInfoSelfViewModel app_UserLogin(string loginID, string secretString, out string Msg,out int Code)
        {

            using (var db = new PeiSongEntities())
            {
                var userInfo = db.P_Person.Single(c => c.PersonID == loginID);
                if (userInfo == null) { Msg = "用户不存在"; }
                string pwd = Encrypt_Helper_SF.UserMd5(secretString.Trim() + "SF_Frame_app_8");
                if (userInfo.PersonPwd != pwd) { Msg = "账号或密码错误"; }
                Msg = "登录成功";
                Code = (int)ReturnCode.OK;
                UserInfoSelfViewModel model = new UserInfoSelfViewModel();
                model.UserID = userInfo.PersonID.ToString();
                model.LoginId = userInfo.LogID;
                model.Phone = userInfo.PersonPhone;
               // model.Address = userInfo.Address;
                model.UserName = userInfo.PersonName;

                return model;
            }

        }

        public List<UserManageViewModel> SelectUsers(string loginId, int userState, int pageIndex, out int pageTotal, out int pageCount,out string Msg,out int Code)
        {
            Expression<Func<P_Person, bool>> where = c => true;
            if (!string.IsNullOrEmpty(loginId))
            {
                where = where.And(c => c.PersonName.Contains(loginId));
            }
            if (userState == 0 || userState == 1)
            {
                where = where.And(c => c.AccountState == userState);
            }

            using (var db = new PeiSongEntities())
            {
                //总条数
                pageTotal = db.P_Person.Where(where).Count();
                //总页数
                pageCount = Convert.ToInt32(Math.Ceiling((decimal)pageTotal / PageSize.Count));
                var list = db.P_Person.Where(where).Skip((pageIndex - 1) * PageSize.Count).Take(PageSize.Count).Select(t => new UserManageViewModel { Uid = t.PersonID, LoginID = t.LogID, Phone = t.PersonPhone, State = Enum.GetName(typeof(OrderState), Convert.ToInt32(t.AccountState)) }).ToList();
                Msg = "操作成功";
                Code = (int)ReturnCode.OK;
                return list;

            }
        }

        //delete user
        public List<UserManageViewModel> deleteUser(string uid,out string Msg,out int Code)
        {
            Msg = "操作失败";Code = (int)ReturnCode.OPERATION_FAILED;
            using (var db = new PeiSongEntities())
            {
                List<UserManageViewModel> reList = new List<UserManageViewModel> ();
                var model = db.P_Person.FirstOrDefault(c => c.PersonID.ToString() == uid);
                model.AccountState = (int)RecordState.DELETE;
                if( db.SaveChanges() > 0)
                {
                    Msg = "操作成功";
                    Code = (int)ReturnCode.OK;
                    reList = db.P_Person.Where(c => c.AccountState == (int)RecordState.NORMAL).Select(c=>new UserManageViewModel() { Uid=c.PersonID.ToString(), LoginID =c.LogID, UserName=c.PersonName,Phone = c.PersonPhone }).ToList();
                }
                
                return reList;
            }
        }
        public List<UserManageViewModel> updateUser(string uid,string loginId,string userName,string phone,out string Msg,out int Code)
        {
            Msg = "操作失败";
            Code = (int)ReturnCode.OPERATION_FAILED;
            using (var db = new PeiSongEntities())
            {
                var model = db.P_Person.FirstOrDefault(c => c.PersonID == uid);
                model.PersonID = loginId;
                model.PersonName = userName;
                model.PersonPhone= phone;
                if(db.SaveChanges() > 0)
                {
                    Msg = "修改成功";
                    Code = (int)ReturnCode.OK;
                }
                List<UserManageViewModel> list = db.P_Person.Where(c => c.AccountState== (int)RecordState.NORMAL).Select(c => new UserManageViewModel() { Uid = c.PersonID, LoginID = c.LogID, UserName = c.PersonName, Phone = c.PersonPhone,State= Enum.GetName(typeof(RecordState),c.AccountState) }).ToList();
                return list;
            }
        }

        //updatePassWord
        public bool updatePassWord(string uid, string pwd,out string Msg)
        {
            using (var db = new PeiSongEntities())
            {
                Msg = "操作失败";
                var model = db.P_Person.FirstOrDefault(c => c.PersonID.ToString() == uid);
                model.PersonPwd = Encrypt_Helper_SF.UserMd5(pwd + "SF_Frame_app_8");
                bool re = db.SaveChanges() > 0;
                if (re) Msg = "修改成功";
                return re;
            }
        }
    }
}
