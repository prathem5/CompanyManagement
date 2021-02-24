using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyManagementBusinessLayer;
using CompanyManagementBusinessLayer.Entities;
using CompanyManagementDatalayer;
using Newtonsoft.Json;

namespace CompanyManagementAPI.Controllers
{

    [Route("api/project")]

    public class ProjectController : ApiController
    {


        private BusinessManager businessManager = new BusinessManager();
        [HttpGet]
      

        public List<BOProject> GetProjects()
        {
            try
            {
               List<BOProject> projectList = businessManager.GetAllProjects().ToList();
                
                return projectList;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        [HttpPost]
       
        public HttpResponseMessage PostProject(Project project)
        {
            HttpResponseMessage response;
            try
            {
                this.businessManager.AddProject(project);
                response = Request.CreateResponse<Project>(HttpStatusCode.Created, project);
            }
            catch (Exception ex)
            {

                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }
        [HttpDelete]
        [Route("api/project/deleteproject")]
        public HttpResponseMessage DeleteProject(int projectId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                businessManager.DeleteProject(projectId);
                // delete student from database 
                string message = ($"Project Deleted - {projectId}");
                response = new HttpResponseMessage(HttpStatusCode.Created);
                response.RequestMessage = new HttpRequestMessage(HttpMethod.Post, message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return response;

        }
        [HttpGet]
        [Route("api/project/employees")]
        public List<BOEmployee> GetEmployeeForProject(int projectID)
        {
            var employeeList = businessManager.GetAllEmployeesForProject(projectID);
            var employeesJsonObj = JsonConvert.SerializeObject(employeeList);
            return employeeList;
        }
        [HttpGet]
        [Route("api/project/delayed")]
        public List<string> GetAllDelayedProjects()
        {
            List<string> projectList = new List<string>();
            foreach (var project in businessManager.GetAllDelayedProjects())
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;


        }

    }
}
