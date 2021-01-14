using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyManagementBusinessLayer;
using CompanyManagementBusinessLayer.Entities;

namespace CompanyManagementAPI.Controllers
{

    public class CompanyMangementUserController : ApiController
    {
        BusinessManager businessManager = new BusinessManager();
        
       
        //Read
        [HttpGet][Route("api/company/Projects")]
        public List<string> GetListOfProject()
        {
            List<string> projectList = new List<string>();
            foreach(var project in businessManager.GetAllProjects())
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;
        }
        [HttpGet][Route("api/company/technologies")]
        public List<string> GetAllTechnologies()
        {
            List<string> technologyList = new List<string>();
            foreach (var technology in businessManager.GetAllTechnologies())
            {
                technologyList.Add(technology.TechnologyName);
            }
            return technologyList;
        }
        [HttpGet][Route("api/company/project/employees")]
        public List<string> GetEmployeeForProject(int projectID)
        {
            List<string> employeeList = new List<string>();
            foreach (var employee in businessManager.GetAllEmployeesForProject(projectID))
            {
                employeeList.Add(employee.EmployeeName);
            }
            return employeeList;
        }
        [HttpGet][Route("api/company/projects/delayed")]
        public List<string> GetAllDelayedProjects()
        {
            List<string> projectList = new List<string>();
            foreach(var project in businessManager.GetAllDelayedProjects())
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;

        }
        [HttpGet]
       
        public List<string> GetAllProjectsForEmployee(int employeeID)
        {
            List<string> projectList = new List<string>();
            foreach(var project in businessManager.GetAllProjectsForEmployee(employeeID))
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;
        }
        [HttpGet]
       

        public List<string> GetAllTasksForEmployee(int employeeID)
        {
            List<string> taskList = new List<string>();
        foreach(var task in businessManager.GetAllTasksForEmployee(employeeID))
            {
                taskList.Add(task.TaskName);

            }
            return taskList;
              
        }
        [HttpGet]
       

        public List<string> GetAllTechnologyTasksForEmployee(int employeeID,int techID)
        {
            List<string> taskList = new List<string>();
            foreach(var task in businessManager.GetAllTechnologyTasksForEmployee(techID,employeeID))
            {
                taskList.Add(task.TaskID.ToString());

            }
            return taskList;
        }
        [HttpGet]
        

        public List<string> GetAllTechnologyProjects(int technologyID)
        {
            List<string> projectList = new List<string>();
              foreach(var project in businessManager.GetAllTechnologyProjects(technologyID))
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;
        }
        [HttpGet]
       
        public List<string> GetAllActiveTasksForProject(int projectID)
        {
            List<string> taskList = new List<string>();
            foreach(var task in businessManager.GetAllActiveTasksForProject(projectID))
            {
                taskList.Add(task.TaskName);

            }
            return taskList;
        }
        [HttpGet]
       
        public List<string> GetAllTechnologiesForEmployee(int employeeID)
        {
            List<string> technologyList = new List<string>();
            foreach(var technology in businessManager.GetAllTechnologiesForEmployee(employeeID))
            {
                technologyList.Add(technology.TechnologyName);
            }
            return technologyList;
        }
        [HttpGet]
        
        public List<string> GetAllActiveProjectsManagedByEmployee(int employeeID)
        {
            List<string> projectList = new List<string>();
            foreach(var project in businessManager.GetAllActiveProjectsManagedByEmployee(employeeID))
            {
                projectList.Add(project.ProjectName);
            }
            return projectList;
        }
        [HttpGet]
      
        public List<string> GetAllDelayedTasksForEmployee(int employeeID)
        {
            List<string> taskList = new List<string>();
            foreach(var task in businessManager.GetAllDelayedTasksForEmployee(employeeID))
            {
                taskList.Add(task.TaskName);

            }
            return taskList; 
        }


    }
}
