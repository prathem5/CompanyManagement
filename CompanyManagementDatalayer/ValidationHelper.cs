using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementDatalayer
{
    public class ValidationHelper
    {
        public string checkCompulsoryClientColumn(Client client)
        {
            

            if (string.IsNullOrEmpty(client.ClientName))
            {
                return QueryResource.ClientNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void InsertClient(Client client,int id,string name, string address,int companyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            
            client.ClientID = id;
            client.ClientName = name;
            client.ClientAddress = address;
            client.CompanyID = companyID;
            string checkColumn = checkCompulsoryClientColumn(client);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Clients.InsertOnSubmit(client);
            dc.SubmitChanges();
        }

        public string checkCompulsoryCompanyColumn(Company company)
        {

            if (string.IsNullOrEmpty(company.CompanyName))
            {
                return QueryResource.CompanyNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void InsertCompany(Company company,int id, string name, string loc)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            company.CompanyID = id;
            company.CompanyName = name;
            company.CompanyAddress = loc;
            string checkColumn = checkCompulsoryCompanyColumn(company);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Companies.InsertOnSubmit(company);
            dc.SubmitChanges();
        }
        public string checkCompulsoryProjectColumn(Project project)
        {

            if (string.IsNullOrEmpty(project.ProjectName))
            {
                return QueryResource.ProjectNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void InsertProject(Project project,int id, string name, int budget, int statusId, int clientId)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            project.ProjectID = id;
            project.ProjectName = name;
            project.ProjectBudget = budget;
            project.StatusID = statusId;
            project.ClientID = clientId;
            string checkColumn = checkCompulsoryProjectColumn(project);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Projects.InsertOnSubmit(project);
            dc.SubmitChanges();
        }
        public string checkCompulsoryTechnologyColumn(TechnologyMaster technology)
        {
            if (string.IsNullOrEmpty(technology.TechName))
            {
                return QueryResource.TechNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void insertTechnology(TechnologyMaster technology,int id, string name, int cost)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            technology.TechID = id;
            technology.TechName = name;
            technology.TechCost = cost;
            string checkColumn = checkCompulsoryTechnologyColumn(technology);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.TechnologyMasters.InsertOnSubmit(technology);
            dc.SubmitChanges();

        }
        public string checkCompulsoryDepartmentColumn(DepartmentMaster department)
        {
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                return QueryResource.DepartmentNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void insertDepartment(DepartmentMaster department,int id, string name, int companyid)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            department.DepartmentID = id;
            department.DepartmentName = name;
            department.CompanyID = companyid;
            string checkColumn = checkCompulsoryDepartmentColumn(department);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.DepartmentMasters.InsertOnSubmit(department);
            dc.SubmitChanges();
        }
        public string checkCompulsoryEmployeeColumn(Employee employee)
        {

            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                return QueryResource.EmployeeNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void insertEmployee(Employee employee,int id, string name, string address, TimeSpan dateOfjoin, TimeSpan dateOfleave, int salary, int deptid)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            employee.EmployeeID = id;
            employee.EmployeeName = name;
            employee.EmployeeAddress = address;
            employee.EmployeeJoined = dateOfjoin;
            employee.EmployeeLeaved = dateOfleave;
            employee.EmployeeSalary = salary;
            employee.DepartmentID = deptid;


            string checkColumn = checkCompulsoryEmployeeColumn(employee);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Employees.InsertOnSubmit(employee);
            dc.SubmitChanges();
        }
        public string checkCompulsoryTaskColumn(Task task)
        { 
            if (string.IsNullOrEmpty(task.TaskName))
            {
                return QueryResource.TaskNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void insertTask(Task task, int id, string name, int statusID)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            task.TaskID = id;
            task.TaskName = name;
            task.StatusID = statusID;
            string checkColumn = checkCompulsoryTaskColumn(task);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }
            dc.Tasks.InsertOnSubmit(task);
            dc.SubmitChanges();
        }
        public string checkCompulsoryStatusColumn(StatusMaster status)
        {
            if (string.IsNullOrEmpty(status.StatusName))
            {
                return QueryResource.TaskNameMissing;
            }
            else
            {
                return QueryResource.AllFieldsPresent;

            }
        }
        public void insertStatus(StatusMaster status,int id, string name)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            status.StatusID = id;
            status.StatusName = name;
            string checkColumn = checkCompulsoryStatusColumn(status);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }

            dc.StatusMasters.InsertOnSubmit(status);
            dc.SubmitChanges();
        }

        public void insertEmployeeProjectMap(EmployeeProjectMap employeeProject,int id, int employeeID, int projectID)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            employeeProject.EmployeeProjectMapID = id;
            employeeProject.EmployeeID = employeeID;
            employeeProject.ProjectID = projectID;
            dc.EmployeeProjectMaps.InsertOnSubmit(employeeProject);
            dc.SubmitChanges();
        }
        public void insertEmployeeTaskMap(EmployeeTaskMap employeeTask,int ID, int employeeId, int taskId)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            employeeTask.EmployeeTaskMapID = ID;
            employeeTask.EmployeeID = employeeId;
            employeeTask.TaskID = taskId;
            dc.EmployeeTaskMaps.InsertOnSubmit(employeeTask);
            dc.SubmitChanges();
        }
        public void insertProjectTaskMap(ProjectTaskMap projectTask,int id, int projectId, int taskId)

        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            projectTask.ProjectTaskMapID = id;
            projectTask.ProjectID = projectId;
            projectTask.TaskID = taskId;
            dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
            dc.SubmitChanges();

        }
        public void insertTechProjectMap(TechProjectMap techProject,int Id, int techID, int projectId)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            techProject.TechProjectMapID = Id;
            techProject.TechID = techID;
            techProject.ProjectID = projectId;
            dc.TechProjectMaps.InsertOnSubmit(techProject);
            dc.SubmitChanges();

        }
        public void insertTechTaskMap(TechTaskMap techTask,int Id, int techId, int taskId)
        {
           CompanyDBDataContext dc = new CompanyDBDataContext();
            techTask.TechTaskMapID = Id;
            techTask.TechID = techId;
            techTask.TaskID = taskId;
        }
    }
}