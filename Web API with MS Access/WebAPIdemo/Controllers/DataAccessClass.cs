using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Configuration;

namespace WebAPIdemo.Controllers
{
    public class DataAccessClass
    {
        string strconnection = ConfigurationManager.ConnectionStrings["AccessDBconnectionString"].ToString();
        OleDbConnection newConnection = new OleDbConnection();
        

    }
}