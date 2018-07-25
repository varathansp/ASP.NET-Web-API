using Https_Protocal_in_WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        //public IEnumerable<Employee> Get()
        //{
        //    using (dbsampleEntities1 EntityObject = new dbsampleEntities1())
        //    {
        //        return EntityObject.Employees.ToList();
        //    }
        //}
        //query string parameter
        public HttpResponseMessage Get(string location = "All")
        {
            try
            {
                
                using (dbsampleEntities EntityObject = new dbsampleEntities())
                {
                    if (location.ToLower() == "all")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, EntityObject.Employees.ToList());
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, EntityObject.Employees.Where(e => e.EmpLocation == location).ToList());
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (dbsampleEntities entityObj = new dbsampleEntities())
                {
                    Employee EmpObj = entityObj.Employees.FirstOrDefault(emp => emp.EmpID == id);
                    if (EmpObj == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with the id=" + id + " not found");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, EmpObj);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Post([FromBody]Employee EmpObj)
        {
            try
            {
                using (dbsampleEntities entityObj = new dbsampleEntities())
                {
                    entityObj.Employees.Add(EmpObj);
                    entityObj.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, EmpObj);
                    message.Headers.Location = new Uri(Request.RequestUri + EmpObj.EmpID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (dbsampleEntities entityObj = new dbsampleEntities())
                {
                    Employee EmpObj = entityObj.Employees.FirstOrDefault(emp => emp.EmpID == id);
                    if (EmpObj == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with the id=" + id + " not found");
                    }
                    else
                    {
                        entityObj.Employees.Remove(EmpObj);
                        entityObj.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Employee EmpObj)
        {
            try
            {
                using (dbsampleEntities entityObj = new dbsampleEntities())
                {
                    Employee Emp = entityObj.Employees.FirstOrDefault(emp => emp.EmpID == id);
                    if (EmpObj == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with the id=" + id + " not found");
                    }
                    else
                    {
                        Emp.EmpName = EmpObj.EmpName;
                        Emp.EmpSalary = EmpObj.EmpSalary;
                        Emp.EmpLocation = EmpObj.EmpLocation;
                        entityObj.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
