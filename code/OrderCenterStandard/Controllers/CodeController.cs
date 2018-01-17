using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Net.Http.Handlers;
using OrderCenterStandard.Common;
using System.Net.Http.Headers;

namespace OrderCenterStandard.Controllers
{
    public class CodeController : ApiController
    {
        // GET: api/Code
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Code
        [HttpGet]
        [Route("api/Code/GetCode")]
        public HttpResponseMessage GetCode()
        {
            try
            {
                var FilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/test.jpg");
                var stream = new FileStream(FilePath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "test.jpg"
                };
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        //文件下载
        // POST: api/Code
        public HttpResponseMessage DownLoadFile()
        {
            try
            {
                var FilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/test.jpg");
                var stream = new FileStream(FilePath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "test.jpg"
                };
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        // PUT: api/Code/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Code/5
        public void Delete(int id)
        {
        }
    }
}
