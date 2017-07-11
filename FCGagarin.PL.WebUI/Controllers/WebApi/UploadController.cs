using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace FCGagarin.PL.WebUI.Controllers.WebApi
{
    public class UploadController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            var file = HttpContext.Current.Request.Files[0];
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Data/uploads/images_gallery"), fileName);

            if (File.Exists(path))
            {
                var input = file.InputStream;
                var output = new FileStream(path, FileMode.Append);
                var buffer = new byte[8 * 1024];
                int len;
                while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, len);
                }
                input.Close();
                output.Close();
            }
            else
            {
                file.SaveAs(path);
            }
            var file1 = new FileInfo(path);
            if (file.ContentLength == file1.Length)
            {

            }


            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new JavaScriptSerializer();
            var result = new { name = file.FileName };

            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}