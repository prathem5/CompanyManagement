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
                return QueryResource.TaskNameMissing;
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

       
    }
}