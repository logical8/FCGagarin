using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HtmlAgilityPack;

namespace Parser.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var url = "http://lfl.ru/club915";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            return doc.DocumentNode.InnerHtml; //new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
