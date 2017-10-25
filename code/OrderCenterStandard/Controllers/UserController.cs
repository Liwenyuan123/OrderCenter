using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderCenterStandard.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IHttpActionResult Get(int pageIndex, int pageSize, bool isDesc)
        {
            throw new Exception();
        }

        // GET: api/User/id
        public IHttpActionResult Get(string id)
        {
            throw new Exception();
        }

        // POST: api/User
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            //Register Opration Guide to UserPhoneRegistion
            throw new Exception();
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
