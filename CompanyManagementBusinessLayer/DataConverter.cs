using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;
using CompanyManagementBusinessLayer.Entities;

namespace CompanyManagementBusinessLayer
{
    public class DataConverter
    {
        public BOProject ProjectConverter(Project project)
        {
            DataManager dataManager = new DataManager();
            BOProject boProject = new BOProject()
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                ProjectBudget = project.ProjectBudget,
                clientID = dataManager.GetClient(project.ClientID),
                statusID = dataManager.GetStatus(project.StatusID)

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
            DataManager dataManager = new DataManager();
            BOClient boClient = new BOClient()
            {
                ClientID = client.ClientID,
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                Company = dataManager.GetCompany((int)client.CompanyID)
            };
            return boClient;
        }
        public BODepartmentMaster DepartmentConverter(DepartmentMaster department)
        {
            DataManager dataManager = new DataManager();
            BODepartmentMaster boDepartmentMaster = new BODepartmentMaster()
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                CompanyID = dataManager.GetCompany(department.CompanyID)
            };
            return boDepartmentMaster;
        }
        public BOEmployee EmployeeConverter(Employee employee)
        {
            DataManager dataManager = new DataManager();
            BOEmployee boEmployee = new BOEmployee()

            {
                EmployeeID = employee.EmployeeID,
                EmployeeName = employee.EmployeeName,
                EmployeeAddress = employee.EmployeeAddress,
                Department = dataManager.GetDepartment(employee.DepartmentID),
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
        public BOTask TaskConverter(CompanyManagementDatalayer.Task task)
        {
            DataManager dataManager = new DataManager();
            BOTask boTask = new BOTask()
            {
                TaskID = task.TaskID,
                TaskName = task.TaskName,
                Status = dataManager.GetStatus(task.StatusID)
            };
            return boTask;

        }
        public BOTechnologyMaster TechnologyConverter(TechnologyMaster technology)
        {
            BOTechnologyMaster boTechnology = new BOTechnologyMaster()
            {
                TechnologyID = technology.TechID,
                TechnologyName = technology.TechName,
                TechnologyCost = technology.TechCost

            };
            return boTechnology;

        }
        public BOTechTaskMap TechTaskConverter(TechTaskMap techTask)
        {

            BOTechTaskMap boTechTask = new BOTechTaskMap()
            {
                TechTaskMapID = techTask.TechTaskMapID,
                TechnologyID = techTask.TechID,
                TaskID = techTask.TaskID

            };
            return boTechTask;

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
        public BOTechTaskMap TechnologyTaskMap(TechTaskMap techTask)
        {
            BOTechTaskMap boTechTask = new BOTechTaskMap()
            {
                TechTaskMapID = techTask.TechTaskMapID,
                TechnologyID = techTask.TechID,
                TaskID = techTask.TaskID
            };
            return boTechTask;
        }
        public List<BOProject> ConvertToBOProjectList(List<Project> projectList)
        {
            List<BOProject> boProjectList = new List<BOProject>();
            foreach (Project project in projectList)
            {
                BOProject bOProject = ProjectConverter(project);
                boProjectList.Add(bOProject);
            }
            return boProjectList;
        }
        public List<BOEmployee> ConvertToBOEmployeeList(List<Employee> employeeList)
        {
            List<BOEmployee> boEmployeeList = new List<BOEmployee>();
            foreach (Employee employee in employeeList)
            {
                BOEmployee boEmployee = EmployeeConverter(employee);
                boEmployeeList.Add(boEmployee);
            }
            return boEmployeeList;
        }
        public List<BOTechnologyMaster> ConvertToBOTechnologyMasterList(List<TechnologyMaster> technologyList)
        {
            List<BOTechnologyMaster> boTechnologies = new List<BOTechnologyMaster>();
            foreach (TechnologyMaster tech in technologyList)
            {
                BOTechnologyMaster boTechnology = TechnologyConverter(tech);
                boTechnologies.Add(boTechnology);
            }
            return boTechnologies;

        }
        public List<BOTask> ConvertToBOTaskList(List<CompanyManagementDatalayer.Task> taskList)
        {
            List<BOTask> boTasks = new List<BOTask>();
            foreach (CompanyManagementDatalayer.Task task in taskList)
            {
                BOTask boTask = TaskConverter(task);
                boTasks.Add(boTask);
            }
            return boTasks;
        }
        public List<BOTechTaskMap> ConvertToBOTechTaskList(List<TechTaskMap> techTaskList)
        {
            List<BOTechTaskMap> boTechTasksList = new List<BOTechTaskMap>();
            foreach (TechTaskMap techTask in techTaskList)
            {
                BOTechTaskMap bOTechTask;
                bOTechTask = TechTaskConverter(techTask);
                boTechTasksList.Add(bOTechTask);
            }

            return boTechTasksList;

        }
    }
}
