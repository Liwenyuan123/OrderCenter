using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace OrderCenterStandard.Models
{
    public class TokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //IEnumerable<string> tokens;
            //var existToken = actionContext.Request.Headers.TryGetValues("token", out tokens);
            //if (existToken)
            //{
            //    var token = tokens.GetEnumerator();
            //    if (token.MoveNext())
            //    {
            //        //解析token，校验是否失效
            //        var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            //        try
            //        {
            //            var jsonPayload = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(token.Current.ToString(), secretKey);
            //        }
            //        catch (JWT.SignatureVerificationException sve)
            //        {
            //            //失效：禁止请求 403
            //            actionContext.Response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.Forbidden };
            //        }
            //        catch (ArgumentException ae)
            //        {
            //            //参数错误
            //            actionContext.Response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.Forbidden };
            //        }
            //        catch (Exception ex)
            //        {
            //            //其他异常 服务器错误：500
            //            actionContext.Response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            //        }
            //    }
            //}
        }
    }
}