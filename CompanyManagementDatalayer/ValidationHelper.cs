using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementDatalayer
{
    public  class ValidationHelper
    {
        public static string checkCompulsoryClientColumn(Client client)
        {


            if (string.IsNullOrEmpty(client.ClientName))
            {
                return QueryResource.ClientNameMissing;
            }
            else if (client.ClientID == 0)
            {
                return QueryResource.ClientIdMissing;
            }
            else if (client.CompanyID == 0)
            {
                return QueryResource.CompanyIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }

        public static string checkCompulsoryCompanyColumn(Company company)
        {

            if (string.IsNullOrEmpty(company.CompanyName))
            {
                return QueryResource.CompanyNameMissing;
            }
            else if (company.CompanyID == 0)
            {
                return QueryResource.CompanyIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryProjectColumn(Project project)
        {

            if (string.IsNullOrEmpty(project.ProjectName))
            {
                return QueryResource.ProjectNameMissing;
            }
            else if (project.StatusID == 0)
            {
                return QueryResource.StatusIdMissing;
            }
            else if (project.ProjectID == 0)
            {
                return QueryResource.ProjectIdMissing;
            }
            else if (project.ClientID == 0)
            {
                return QueryResource.ClientIdMissing;
            }

            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryTechnologyColumn(TechnologyMaster technology)
        {
            if (string.IsNullOrEmpty(technology.TechName))
            {
                return QueryResource.TechNameMissing;
            }
            else if (technology.TechID == 0)
            {
                return QueryResource.TechIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryDepartmentColumn(DepartmentMaster department)
        {
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                return QueryResource.DepartmentNameMissing;
            }
            else if (department.DepartmentID == 0)
            {
                return QueryResource.DepartmenIDtMissing;
            }
            else if (department.CompanyID == 0)
            {
                return QueryResource.CompanyIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryEmployeeColumn(Employee employee)
        {

            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                return QueryResource.EmployeeNameMissing;
            }
            else if (employee.EmployeeID == 0)
            {
                return QueryResource.EmployeeIdMissing;
            }
            else if (employee.EmployeeSalary == 0)
            {
                return QueryResource.EmployeeSalaryMissing;
            }
            else if (employee.EmployeeJoined ==  null)
            {
                return QueryResource.JoinDateMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryTaskColumn(Task task)
        {
            if (string.IsNullOrEmpty(task.TaskName))
            {
                return QueryResource.TaskNameMissing;
            }
            else if (task.TaskID == 0 )
            {
                return QueryResource.TaskIdMissing;
            }
            else if (task.StatusID == 0)
            {
                return QueryResource.StatusIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryStatusColumn(StatusMaster status)
        {
            if (string.IsNullOrEmpty(status.StatusName))
            {
                return QueryResource.StatusNameMissing;
            }
            else if (status.StatusID == 0)
            {
                return QueryResource.StatusIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryRolesColumn(RoleMaster role)
        {
            if (string.IsNullOrEmpty(role.RoleName))
            {
                return QueryResource.RoleNameMissing;
            }
            else if (role.RoleID == 0)
            {
                return QueryResource.RoleIDMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }

        public static string checkCompulsoryEmployeeProjectColumn(EmployeeProject employeeProject)
        {
            if (employeeProject.EmployeeProjectMapID==0)
            {
                return QueryResource.EmployeeProjectMapIDMissing;
            }
            else if (employeeProject.ProjectID == 0)
            {
                return QueryResource.ProjectIdMissing;
            }
            else if (employeeProject.EmployeeID == 0)
            {
                return QueryResource.EmployeeIdMissing;
            }
            else if (employeeProject.RoleID == 0)
            {
                return QueryResource.RoleIDMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string CheckCompulsoryEmployeeTaskColumn(EmployeeTaskMap employeeTask)
        {
            if (employeeTask.EmployeeTaskMapID == 0)
            {
                return QueryResource.EmpTaskIDMissing;
            }
            else if (employeeTask.TaskID == 0)
            {
                return QueryResource.ProjectIdMissing;
            }
            else if (employeeTask.EmployeeID == 0)
            {
                return QueryResource.EmployeeIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string CheckCompulsoryProjectTaskColumn(ProjectTaskMap projectTask)
        {
            if (projectTask.ProjectTaskMapID == 0)
            {
                return QueryResource.ProjectTaskIDMissing;
            }
            else if (projectTask.ProjectID == 0)
            {
                return QueryResource.ProjectIdMissing;
            }
            else if (projectTask.TaskID == 0)
            {
                return QueryResource.TaskIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryTechProjectColumn(TechProjectMap techProject)
        {
            if (techProject.TechProjectMapID == 0)
            {
                return QueryResource.TechProjectIDMissing;
            }
            else if (techProject.ProjectID == 0)
            {
                return QueryResource.ProjectIdMissing;
            }
            else if (techProject.TechID == 0)
            {
                return QueryResource.TechIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static string checkCompulsoryTechtaskColumn(TechTaskMap techTask)
        {
            if (techTask.TechTaskMapID == 0)
            {
                return QueryResource.TechTaskMapIDMissing;
            }
            else if (techTask.TaskID == 0)
            {
                return QueryResource.TaskIdMissing;
            }
            else if (techTask.TechID == 0)
            {
                return QueryResource.TechIdMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public static bool IfProjectExist(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var projectTest = (from project in dc.Projects select project.ProjectID).ToList();
            if (projectTest.Contains(projectID))
            {
                return true;
            }
            return false; 

        }
        public static bool IfEmployeeExist(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var employeeTest = (from emp in dc.Employees select emp.EmployeeID).ToList();
            if (employeeTest.Contains(employeeID))
            {
                return true;
            }
            return false;

            
        }   
        public static bool IfTaskExist(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var taskTest = (from task in dc.Tasks select task.TaskID).ToList();
            if (taskTest.Contains(taskID))
            {
                return true;

            }
            return false;
        }
        public static bool IftechnologyExist(int technologyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var technologyTest = (from tech in dc.TechnologyMasters select tech.TechID).ToList();
            if (technologyTest.Contains(technologyID))
            {
                return true;
            }
            return false;
        }
        public static bool isManager(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            EmployeeProject employee = (from emp in dc.EmployeeProjects where emp.EmployeeID == employeeID select emp).First();
            if (employee.RoleID == (int)RoleEnum.ProjectManager)
            {
                return true;
            }
            return false;
        }
        public static bool isWorker(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            EmployeeProject employee = (from emp in dc.EmployeeProjects where emp.EmployeeID == employeeID select emp).First();
            if (employee.RoleID == (int)RoleEnum.Worker)
            {
                return true;
            }
            return false;
        }
    }
}