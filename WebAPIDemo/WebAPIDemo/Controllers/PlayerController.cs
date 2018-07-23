using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayerDataAccess;

namespace WebAPIDemo.Controllers
{
    public class PlayerController : ApiController
    {
        public IEnumerable<Player> Get()
        {
            using (dbsampleEntities EntityObject = new dbsampleEntities())
            {
              return  EntityObject.Players.ToList();
            }
        }
        //public Player Get(int id)
        //{
        //    using (dbsampleEntities EntityObject = new dbsampleEntities())
        //    {
        //        return EntityObject.Players.FirstOrDefault(pl => pl.Salary == id);
        //    }
        //}
    }
}
