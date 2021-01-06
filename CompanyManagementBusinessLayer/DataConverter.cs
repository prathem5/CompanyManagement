using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;
using CompanyManagementBusinessLayer.Entities;

namespace CompanyManagementBusinessLayer
{
    class DataConverter
    {
        public BOProject ProjectConverter(Project project)
        {
            BOProject boProject = new BOProject()
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                ProjectBudget = project.ProjectBudget,
                
                
            };
            return boProject;

        }
        public BOCompany CompanyConverter(Company company)
        {
            BOCompany boCompany = new BOCompany()
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
                CompanyAddress = company.CompanyAddress
            };
            return boCompany;
        }
        public BOClient ClientConverter(Client client)
        {

            BOClient boClient = new BOClient()
            {
                ClientID = client.ClientID,
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                CompanyID =  (int) client.CompanyID
            };
            return boClient;
        }
        public BODepartmentMaster DepartmentConverter (DepartmentMaster department)
        {
            BODepartmentMaster boDepartmentMaster = new BODepartmentMaster()
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                CompanyID = department.CompanyID
            };
            return boDepartmentMaster;
        }
        public BOEmployee EmployeeConverter(Employee employee)
        {
            BOEmployee boEmployee = new BOEmployee()
            {
                EmployeeID = employee.EmployeeID,
                EmployeeName = employee.EmployeeName,
                EmployeeAddress = employee.EmployeeAddress,
                DepartmentID = employee.DepartmentID,
                EmployeeSalary = employee.EmployeeSalary
            };
            return boEmployee;
        }
        public BOStatusMaster StatusConverter(StatusMaster status)
        {
            BOStatusMaster boStatus = new BOStatusMaster()
            {
                StatusId = status.StatusID,
                StatusName = status.StatusName
            };
            return boStatus;

        }
        public BoTask TaskConverter(CompanyManagementDatalayer.Task task)
        {
            BoTask boTask = new BoTask()
            {
                TaskID = task.TaskID,
                TaskName = task.TaskName,
                StatusID = task.StatusID
            };
            return boTask;

        }
        public BOTechnologyMaster TechnologyConverter( TechnologyMaster technology)
        {
            BOTechnologyMaster boTechnology = new BOTechnologyMaster()
            {
                TechnologyID = technology.TechID,
                TechnologyName = technology.TechName,
                TechnologyCost = technology.TechCost
            };
            return boTechnology;
            
        }
        public BOEmployeeProjectMap EmployeeProjectMap(EmployeeProject empProject)
        {
            BOEmployeeProjectMap boEmployeeProject = new BOEmployeeProjectMap()
            {
                EmployeeProjectMapID = empProject.EmployeeProjectMapID,
                EmployeeID = empProject.EmployeeID,
                ProjectID = empProject.ProjectID
            };
            return boEmployeeProject;
        }
        public BOEmployeeTaskMap EmployeeTaskMap(EmployeeTaskMap empTask)
        {
            BOEmployeeTaskMap boEmployeeTask = new Entities.BOEmployeeTaskMap()
            {
                EmployeeTaskID = empTask.EmployeeTaskMapID,
                EmployeeID = empTask.EmployeeID,
                TaskId = empTask.TaskID
            };
            return boEmployeeTask;
        }
        public BOProjectTaskMap ProjectTaskMap(ProjectTaskMap projectTask)
        {
            BOProjectTaskMap boProjectTask = new BOProjectTaskMap()
            {
                ProjecTaskMapID = projectTask.ProjectTaskMapID,
                ProjectID = projectTask.ProjectID,
                TaskID = projectTask.TaskID
            };
            return boProjectTask;
        }
        public BoTechTaskMap  TechnologyTaskMap(TechTaskMap techTask)
        {
            BoTechTaskMap boTechTask = new BoTechTaskMap()
            {
                TechTaskMapID = techTask.TechTaskMapID,
                TechnologyID = techTask.TechID,
                TaskID = techTask.TaskID
            };
            return boTechTask;
        }

    }
}
