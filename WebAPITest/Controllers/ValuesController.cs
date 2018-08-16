using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPITest.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        static List<string> Sample =  new List<string>()
        {
            "value0","value1", "value2"
        };
        // GET api/values
        public IEnumerable<string> Get()
        {
            return Sample;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return Sample[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            Sample.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            Sample[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Sample.RemoveAt(id);
        }
    }
}
