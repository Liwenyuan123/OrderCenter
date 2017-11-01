﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SF_Frame_Valudation;
using OrderCenter.Data.DTO;
using OrderCenter.Data.Service;

namespace OrderCenterStandard.Controllers
{
    public class UserController : ApiController
    {
        UserService serivice = new UserService();
        // GET: api/User
        public IHttpActionResult Get(string loginID,string userState, int pageIndex, int pageSize)
        {
            throw new Exception();
        }

        // GET: api/User/id
        public IHttpActionResult Get(string id)
        {
            throw new Exception();
        }

        // POST: api/UserLogin
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //check args
            SF_Frame_Valudation.CheckDataNullValudation.CheckNullGet(query, "数据为空");
            string loginId = query.loginId;
            int random = query.random;
            long timeStamp = query.timeStamp;
            string secretString =query.secretString;

            //login and get user info
           
           //support loginid,phone,email login!
             LoginDataModel loginDataModel = serivice.UserLogin(loginId, random, timeStamp, secretString);
             return Ok(loginDataModel);
        }

        // PUT: api/User
        public IHttpActionResult Put([FromBody]dynamic query)
        {
            throw new Exception();
        }

        // DELETE: api/User
        public IHttpActionResult Delete([FromBody]dynamic query)
        {
            throw new Exception();
        }
    }
}
