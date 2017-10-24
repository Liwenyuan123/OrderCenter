using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderCenterStandard.Services;
using Newtonsoft.Json;
using OrderCenterStandard.ViewModels;
using Newtonsoft.Json.Linq;

namespace OrderCenterStandard.Controllers
{
    public class OrderController : ApiController
    {
        //api/Order
        public IHttpActionResult Post([FromBody]dynamic query)
        {

            //OrderView orderV = new OrderView();
            //orderV.OrderMain = JsonConvert.DeserializeObject<OrderView>(query.OrderMain);
            //orderV.OrderDetail = JsonConvert.DeserializeObject<OrderView>(query.OrderDetail);
            //O_OrderMain oMain = orderV.OrderMain;
            //List<O_OrderDetail> oDetail = orderV.OrderDetail;

            //var service = new OrderService();
            //service.AddOrder(oMain, oDetail);

            JObject googleSearch = JObject.Parse(query);

            // get JSON result objects into a list
            IList<JToken> results = googleSearch["OrderDetail"].Children().ToList();
            O_OrderMain orderM = googleSearch["OrderMain"].ToObject<O_OrderMain>();
            // serialize JSON results into .NET objects
            IList<O_OrderDetail> searchResults = new List<O_OrderDetail>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                O_OrderDetail searchResult = result.ToObject<O_OrderDetail>();
                searchResults.Add(searchResult);
            }
            var service = new OrderService();
            service.AddOrder(orderM, searchResults.ToList());
            return Ok();
        }
    }
}
