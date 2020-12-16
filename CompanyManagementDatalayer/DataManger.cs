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

        public List<Project> getAllProjects()
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> getProjectList = (from p in dc.Projects
                                          select p).ToList();
            return getProjectList;
        }
           
        public List<TechnologyMaster> getAllTechologies()
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<TechnologyMaster> techlist = (from t in dc.TechnologyMasters
                                               select t).ToList();
            return techlist;
        }
        public int GetEmployeeCountForProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> getEmployeeCount = (from emp in dc.EmployeeProjectMaps
                                              where emp.ProjectID == projectID 
                                              select emp.Project).ToList();
            return getEmployeeCount.Count;
        }
        public List<Employee> GetEmployeesForProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Employee> getEmployee = (from emp in dc.EmployeeProjectMaps
                                          where emp.ProjectID == projectID 
                                          select emp.Employee).ToList();
           
            return getEmployee;
        }
       
        public List<Project> GetAllDelayedProjects()
        {

            CompanyDBDataContext dc = new CompanyDBDataContext();    
            List<Project> delayedProjects = (from p in dc.Projects
                                             where p.StatusID == (int)StatusEnum.Delayed 
                                          select p).ToList();

            return delayedProjects;
        }
        public List<Project> GetAllProjectsForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> allProjects = (from emp in dc.EmployeeProjectMaps where emp.EmployeeID == employeeID select emp.Project).ToList();
            return allProjects;
        }
        public List<Task> GetAllTasksForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Task> EmpTask = (from emp in dc.EmployeeTaskMaps 
                                  where emp.EmployeeID == employeeID 
                                  select emp.Task).ToList();
            return EmpTask;
        }
        //*************************************************************************************************************
        public List<(TechnologyMaster Technology,Task Task,Employee Employee)> GetAllTechnologyTasksForEmployee(int technologyID, int employeeID)
                {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var allTech = (from et in dc.EmployeeTaskMaps 
                                             join tt in dc.TechTaskMaps on et.TaskID equals tt.TaskID 
                                             where et.EmployeeID == employeeID && tt.TechID ==technologyID
                                             select new {Technology = tt.TechnologyMaster, Task =tt.Task ,Employee = et.Employee}).ToList();
            return allTech.Select(r => (r.Technology, r.Task, r.Employee)).ToList() ;
        }
        public List<Project> GetAllTechnologyProjects(int technologyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> techProject = (from tech in dc.TechProjectMaps
                                   where tech.TechID == technologyID
                                   select tech.Project ).ToList();
           
             return techProject ;
        }
        public List<Task> GetAllActiveTasksForProject(int projectID)
        {
            CompanyDBDataContext dc = new  CompanyDBDataContext();
            List<Task> taskProjectQuery = (from project in dc.ProjectTaskMaps 
                                           where project.ProjectID == projectID && project.Task.StatusID==(int)StatusEnum.Active 
                                           select project.Task).ToList();
                                   
                return taskProjectQuery;
        }
        //**********************************************************************************************************
        public List<(TechnologyMaster Technology,Employee Employee)> GetAllTechnologiesForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var allTech = (from et in dc.EmployeeTaskMaps
                           join tt in dc.TechTaskMaps on et.TaskID equals tt.TaskID
                           where et.EmployeeID == employeeID 
                           select new { Technology = tt.TechnologyMaster, Task = tt.Task, Employee = et.Employee }).ToList();
            return allTech.Select(r => (r.Technology,  r.Employee)).ToList();


        }

        public int GetProjectCountForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> projectCountQuery = (from emp in dc.EmployeeProjectMaps 
                                               where emp.EmployeeID == employeeID 
                                               select emp.Project).ToList();
            
            return projectCountQuery.Count;
        }
        public List<Project> GetAllActiveProjectsManagedByEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> activeProjectsQuery = (from emp in dc.EmployeeProjectMaps
                                      where emp.EmployeeID == employeeID && emp.Project.StatusID == (int)StatusEnum.Active
                                      select emp.Project).ToList();
            return activeProjectsQuery;
        }
        public List<Task> GetAllDelayedTasksForEmployee(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Task> delayedTaskQuery = (from emp in dc.EmployeeTaskMaps 
                                   where emp.EmployeeID == employeeID && emp.Task.StatusID == (int)StatusEnum.Delayed 
                                   select emp.Task).ToList();
            return delayedTaskQuery;
          }
     public void AddProject(Project project)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            dc.Projects.InsertOnSubmit(project);
            dc.SubmitChanges(); 
        }

        public void AddTechnology(TechnologyMaster technology)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            dc.TechnologyMasters.InsertOnSubmit(technology);
            dc.SubmitChanges();
        }
       public void  AddEmployee(Employee employee)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            dc.Employees.InsertOnSubmit(employee);
            dc.SubmitChanges();
        }
       public void AssignEmployeeToProject(int employeeID, int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var project = (from emp in dc.EmployeeProjectMaps 
                           where emp.EmployeeID == employeeID 
                           select emp).First();
            project.ProjectID = projectID;
            dc.SubmitChanges();
        }
       public void  CreateTaskInProject(Task task, int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            var createTask = (from t in dc.ProjectTaskMaps
                             join  p in dc.Projects on p.ProjectID equals t.ProjectID into projecttask
                             from pt in projecttask
                             select pt).tol
            
        }
        public void AssignTechnologyToTask(int technologyID, int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            
            var assignTech = (from task in dc.TechTaskMaps where task.TaskID == taskID select task).First();
            assignTech.TechID = technologyID;
        }
        //***************************************************************************************************
        public void UpdateTechnologiesForTask(List<int> technologyIDs, int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var updateTechTask = (from task in dc.TechTaskMaps where task.TaskID == taskID select new { task.TechID = technologyIDs }).ToList();
           

        }
        public void DeleteEmployeeFromSystem(int employeeID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var deleteEmployee = (from emp in dc.Employees 
                                  where emp.EmployeeID == employeeID 
                                  select emp).ToList();
            foreach(Employee emp in deleteEmployee)
            {
                dc.Employees.DeleteOnSubmit(emp);
            }
            dc.SubmitChanges();
        }
        public void DeleteTechnology(int technology)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var deleteTechnology = (from tech in dc.TechnologyMasters
                                    where tech.TechID == technology
                                    select tech).ToList();
            foreach(TechnologyMaster tech in deleteTechnology)
            {
                dc.TechnologyMasters.DeleteOnSubmit(tech);
            }
            dc.SubmitChanges();

        }
        public void DeleteTask(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var deleteTask = (from task in dc.Tasks
                              where task.TaskID == taskID 
                              select task).ToList();
            foreach(Task task in deleteTask)
            {
                dc.Tasks.DeleteOnSubmit(task);

            }
            dc.SubmitChanges();
        }
        public void DeleteProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var deleteProject = (from project in dc.Projects 
                                 where project.ProjectID == projectID 
                                 select project).ToList();   
            foreach(Project project in deleteProject)
            {
                dc.Projects.DeleteOnSubmit(project);

            }
            dc.SubmitChanges();
        }





    }
}
