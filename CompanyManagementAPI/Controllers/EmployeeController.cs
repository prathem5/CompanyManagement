using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyManagementBusinessLayer;
using CompanyManagementBusinessLayer.Entities;
using Newtonsoft.Json;
using CompanyManagementDatalayer;
namespace CompanyManagementAPI.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : ApiController
    {
        BusinessManager businessManager = new BusinessManager();
       
        [HttpGet]
        [Route("api/employee/projects")]
        public List<string> GetAllProjectsForEmployee(int employeeID)
        {
            List<string> projectList = new List<string>();
            foreach (var project in businessManager.GetAllProjectsForEmployee(employeeID))
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;
        }
        [HttpGet]
        [Route("api/employee/tasks")]
        public List<string> GetAllTasksForEmployee(int employeeID)
        {
            List<string> taskList = new List<string>();
            foreach (var task in businessManager.GetAllTasksForEmployee(employeeID))
            {
                taskList.Add(task.TaskName);

            }
            return taskList;
        }/// <summary>
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="techID"></param>
        /// <returns></returns>
        [HttpGet][Route("api/employee/technology")]
        public List<string> GetAllTechnologyTasksForEmployee(int employeeID, int techID)
        {
            List<string> taskList = new List<string>();
            foreach (var task in businessManager.GetAllTechnologyTasksForEmployee(techID, employeeID))
            {
                taskList.Add(task.TaskID.ToString());

            }
            return taskList;
        }
        [HttpGet][Route("api/employee/technologies")]
        public List<string> GetAllTechnologiesForEmployee(int employeeID)
        {
            List<string> technologyList = new List<string>();
            foreach (var technology in businessManager.GetAllTechnologiesForEmployee(employeeID))
            {
                technologyList.Add(technology.TechnologyName);
            }
            return technologyList;
        }
        [HttpGet]
        [Route("api/employee/addemplpoyee")]

        public IHttpActionResult AddEmployee(Employee employee)
        {
            businessManager.AddEmployee(employee);
            var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);
            return Ok( response);
        }
        [HttpDelete]
        [Route("api/employee/delete")]
        public HttpResponseMessage DeleteEmployee(int employeeID)
        {
            businessManager.DeleteEmployeeFromSystem(employeeID);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

    }
}
