using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
       static List<string> playerlist = new List<string>() {"De Gea","Paul Pogba","Lukaku" ,"Shaw"};
        // GET api/values
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return playerlist;
        }

        // GET api/values/5
        public string Get(int id)
        {
            //return "value";
            return playerlist[id];
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            playerlist.Add(value);
            return "Added";
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            playerlist[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            playerlist.RemoveAt(id);
        }
    }
}
