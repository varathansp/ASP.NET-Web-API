using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace WebAPIdemo.Controllers
{
    public class EmployeeController : ApiController
    {

        string strconnection = ConfigurationManager.ConnectionStrings["AccessDBconnectionString"].ToString();


        public IEnumerable<DataRow> Get()
        {
            OleDbConnection newConnection = new OleDbConnection(strconnection);
            string query = "select * from EmployeeTable";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, newConnection);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
          
            return table1.AsEnumerable();

        }
        //public HttpResponseMessage Get(int id)
        //{
        //    OleDbConnection newConnection = new OleDbConnection(strconnection);
        //    string query = "select * from EmployeeTable where ID="+id+"";
        //    OleDbCommand cmd = new OleDbCommand(query,newConnection);
        //    OleDbDataReader reader = cmd.ExecuteReader();
           

        //    return reader.;

        //}

        public HttpResponseMessage Post([FromBody]EmployeeTable employee)
        {
            try
            {
                OleDbConnection newConnection = new OleDbConnection(strconnection);
                string InsertQuery = "insert into EmployeeTable ([EmpName],[Salary],[Location]) values ('" + employee.EmpName + "'," + employee.Salary + ",'" + employee.Location + "')";
                OleDbCommand cmd = new OleDbCommand(InsertQuery, newConnection);

                newConnection.Open();
                cmd.ExecuteNonQuery();
                newConnection.Close();
                var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                return message;
            }
            catch (Exception ex)
            {
              return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {

                OleDbConnection newConnection = new OleDbConnection(strconnection);
                string DeleteQuery = "delete from EmployeeTable where ID=" + id + "";
                OleDbCommand cmd = new OleDbCommand(DeleteQuery, newConnection);

                newConnection.Open();
                int ans = cmd.ExecuteNonQuery();
                newConnection.Close();
                if (ans == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with the id=" + id + " is not found");
                }
                else return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id,[FromBody]EmployeeTable employee)
        {
            try
            {

                OleDbConnection newConnection = new OleDbConnection(strconnection);
                string UpdateQuery = "update EmployeeTable set [EmpName]='" + employee.EmpName + "',[Salary]=" + employee.Salary + ",[Location]='" + employee.Location + "' where ID=" + id + "";
                OleDbCommand cmd = new OleDbCommand(UpdateQuery, newConnection);

                newConnection.Open();
                int ans = cmd.ExecuteNonQuery();
                newConnection.Close();
                if (ans == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with id='" + id + "' not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Updated");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
