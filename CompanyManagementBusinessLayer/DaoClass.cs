using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;
using CompanyManagementBusinessLayer.Entities;

namespace CompanyManagementBusinessLayer
{
    class DaoClass
    {
        public BOProject BOProjectDao(Project project)
        {
            BOProject boProject = new BOProject()
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                ProjectBudget = project.ProjectBudget,
                StatusID = project.StatusID,
                ClientID = project.ClientID
            };
            return boProject;

        }
        public BOCompany BOCompanyDao(Company company)
        {
            BOCompany boCompany = new BOCompany()
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
                CompanyAddress = company.CompanyAddress
            };
            return boCompany;
        }
        public BOClient BOClientDao(Client client)
        {

            BOClient boClient = new BOClient()
            {
                ClientID = client.ClientID,
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                CompanyID = (int)client.CompanyID
            };
            return boClient;
        }
        public BODepartmentMaster BODepartmentDao(DepartmentMaster department)
        {
            BODepartmentMaster boDepartmentMaster = new BODepartmentMaster()
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                CompanyID = department.CompanyID
            };
            return boDepartmentMaster;
        }
        public BOEmployee BOEmployeeDao(Employee employee)
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
        public BOStatusMaster BOStatusDao(StatusMaster status)
        {
            BOStatusMaster boStatus = new BOStatusMaster()
            {
                StatusId = status.StatusID,
                StatusName = status.StatusName
            };
            return boStatus;

        }
        public BoTask BOTaskDao(CompanyManagementDatalayer.Task task)
        {
            BoTask boTask = new BoTask()
            {
                TaskID = task.TaskID,
                TaskName = task.TaskName,
                StatusID = task.StatusID
            };
            return boTask;

        }
        public BOTechnologyMaster BOTechnologyDao( TechnologyMaster technology)
        {
            BOTechnologyMaster boTechnology = new BOTechnologyMaster()
            {
                TechnologyID = technology.TechID,
                TechnologyName = technology.TechName,
                TechnologyCost = technology.TechCost
            };
            return boTechnology;
            
        }
        public BOEmployeeProjectMap BOEmployeeProjectMap(EmployeeProject empProject)
        {
            BOEmployeeProjectMap boEmployeeProject = new BOEmployeeProjectMap()
            {
                EmployeeProjectMapID = empProject.EmployeeProjectMapID,
                EmployeeID = empProject.EmployeeID,
                ProjectID = empProject.ProjectID
            };
            return boEmployeeProject;
        }
        public BOEmployeeTaskMap BOEmployeeTaskMap(EmployeeTaskMap empTask)
        {
            BOEmployeeTaskMap boEmployeeTask = new Entities.BOEmployeeTaskMap()
            {
                EmployeeTaskID = empTask.EmployeeTaskMapID,
                EmployeeID = empTask.EmployeeID,
                TaskId = empTask.TaskID
            };
            return boEmployeeTask;
        }
        public BOProjectTaskMap BOProjectTaskMap(ProjectTaskMap projectTask)
        {
            BOProjectTaskMap boProjectTask = new BOProjectTaskMap()
            {
                ProjecTaskMapID = projectTask.ProjectTaskMapID,
                ProjectID = projectTask.ProjectID,
                TaskID = projectTask.TaskID
            };
            return boProjectTask;
        }
        public BoTechTaskMap BOTechnologyTaskMap(TechTaskMap techTask)
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
