using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CompanyManagementDatalayer
{
    class DataManger
    {
        
        public void getAllProjects()
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<Project> projects = dc.Projects;
            var projectQuery = from p in projects select p.ProjectName;
            foreach (string p in projectQuery)
            {
                Console.WriteLine(p);
            }
        }
        public void getAllTechologies()
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<TechnologyMaster> technologies = dc.TechnologyMasters;
            var techlist = from t in technologies select t.TechName;
            foreach (string t in techlist)
            {
                Console.WriteLine(t);
            }
        }
        public int GetEmployeeCountForProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<EmployeeProjectMap> employees = dc.EmployeeProjectMaps;
            var empcountQuery = from emp in employees where emp.ProjectID == projectID select emp.EmployeeID;
            int count = 0;
            foreach (int e in empcountQuery)
            {
                count++;
            }
            return count;
        }
        public List<string> GetEmployeesForProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<EmployeeProjectMap> employees = dc.EmployeeProjectMaps;
            var empcountQuery = from emp in employees where emp.ProjectID == projectID select emp.Employee.EmployeeName;
            List<string> EmpName = new List<string>();
            foreach (string name in empcountQuery)
            {
                EmpName.Add(name);
            }
            return EmpName;
        }
       
        public List<int> GetAllDelayedProjects()
        {

            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<Project> projects = dc.Projects;
            

            var projectQuery = from p in projects where p.StatusID == (int) StatusEnum.Delayed select p.ProjectID;
            List<int> projectlist = new List<int>();
            foreach (var project in projectQuery)
            {
                projectlist.Add(project);
            }
            return projectlist;
        }
        public List<int> GetAllProjectsForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<EmployeeProjectMap> employeeProjects = dc.EmployeeProjectMaps;
            var employeeProjectQuery = from emp in employeeProjects where emp.EmployeeID == employeeID select emp.ProjectID;
            List<int> projectList = new List<int>();
            foreach (int project in employeeProjectQuery)
            {
                projectList.Add(project);
            }
            return projectList;
        }
        public IQueryable<Task> GetAllTasksForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<EmployeeTaskMap> employeeTasks = dc.EmployeeTaskMaps;
            var EmpTaskQuery = from emp in employeeTasks where emp.EmployeeID == employeeID select emp.Task;

            return EmpTaskQuery;
        }
        public string GetAllTechnologyTasksForEmployee(int technologyID, int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<Task> techTaskMaps = dc.Tasks;
            var techTaskQuery = from task in techTaskMaps select task.TaskName;

            return techTaskQuery;
        }
        public IQueryable<int> GetAllTechnologyProjects(int technologyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<TechProjectMap> techProjects = dc.TechProjectMaps;
            var techProjectQuery = from tech in techProjects
                                   where tech.TechID == technologyID
                                   select tech.TechProjectMapID ;
             return techProjectQuery ;
        }
        public IQueryable<Task> GetAllActiveTasksForProject(int projectID)
        {
            CompanyDBDataContext dc = new  CompanyDBDataContext();
            Table<ProjectTaskMap> taskProjects = dc.ProjectTaskMaps;
            var taskProjectQuery = from project in taskProjects where project.ProjectID == projectID && project.Task.StatusID==(int)StatusEnum.Active select project.Task;
                                   
                return taskProjectQuery;
        }
        public IQueryable<TechProjectMap> GetAllTechnologiesForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table < EmployeeProjectMap > employeeProjects = dc.EmployeeProjectMaps;
            var techEmployeeQuery = from emp in employeeProjects where emp.EmployeeID == employeeID select emp.Project.TechProjectMaps;
            return techEmployeeQuery;
        }

        public int GetProjectCountForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table < EmployeeProjectMap > employeeProjects= dc.EmployeeProjectMaps;
            var projectCountQuery = from emp in employeeProjects where emp.EmployeeID == employeeID select emp.ProjectID;
            int count = 0;
            foreach (int i in projectCountQuery)
            {
                count++;
            }
            return count;
        }
        public IQueryable<Project> GetAllActiveProjectsManagedByEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<EmployeeProjectMap> employeeProjects = dc.EmployeeProjectMaps;
            var activeProjectsQuery = from emp in employeeProjects where emp.EmployeeID == employeeID && emp.Project.StatusID == (int)StatusEnum.Active select emp.Project;
            return activeProjectsQuery;
        }
        public IQueryable<Task> GetAllDelayedTasksForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Table<EmployeeTaskMap> employeeTasks = dc.EmployeeTaskMaps;
            var delayedTaskQuery = from emp in employeeTasks where emp.EmployeeID == employeeID && emp.Task.StatusID == (int)StatusEnum.Delayed select emp.Task;
            return delayedTaskQuery;
          }
     public void AddProject(Project project)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            dc.Projects.InsertOnSubmit(project);
            dc.SubmitChanges();
        }


    }
}
