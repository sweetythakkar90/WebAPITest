using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebAPITest.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public IEnumerable<Employee> LoadAllEmployees()
        {
            EmployeeDBEntities entities = new EmployeeDBEntities();
            return entities.Employees.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            EmployeeDBEntities entities = new EmployeeDBEntities();
            var entity =  entities.Employees.FirstOrDefault(e => e.ID == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Employee with ID = " + id.ToString() + " not found");
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                EmployeeDBEntities entities = new EmployeeDBEntities();
                entities.Employees.Add(employee);
                entities.SaveChanges();
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
                EmployeeDBEntities entities = new EmployeeDBEntities();
                Employee empIdToRemove = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (empIdToRemove == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with ID " + id.ToString() + " not found");
                }
                else
                {
                    entities.Employees.Remove(empIdToRemove);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                EmployeeDBEntities entities = new EmployeeDBEntities();
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
               if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found with ID " + id + ".");
                }
               else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;
                    entities.SaveChanges();
                   return Request.CreateResponse(HttpStatusCode.Created, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
