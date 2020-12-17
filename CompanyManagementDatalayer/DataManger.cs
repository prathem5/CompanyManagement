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
        ValidationHelper validate = new ValidationHelper();
        public List<Project> getAllProjects()
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> getAllProjectList = (from project in dc.Projects
                                          select project).ToList();
            return getAllProjectList;
        }
           
        public List<TechnologyMaster> getAllTechologies()
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<TechnologyMaster> getAllTechlist = (from tech in dc.TechnologyMasters
                                               select tech).ToList();
            return getAllTechlist;
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
            List<Project> delayedProjects = (from project in dc.Projects
                                             where project.StatusID == (int)StatusEnum.Delayed 
                                          select project).ToList();

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
            var allTech = (from employeeTask in dc.EmployeeTaskMaps 
                                             join techTask in dc.TechTaskMaps on employeeTask.TaskID equals techTask.TaskID 
                                             where employeeTask.EmployeeID == employeeID && techTask.TechID ==technologyID
                                             select new {Technology = techTask.TechnologyMaster, Task =techTask.Task ,Employee = employeeTask.Employee}).ToList();
            return allTech.Select(tuple => (tuple.Technology, tuple.Task, tuple.Employee)).ToList() ;
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
            var allTech = (from employeeTask in dc.EmployeeTaskMaps
                           join techTask in dc.TechTaskMaps on employeeTask.TaskID equals techTask.TaskID
                           where employeeTask.EmployeeID == employeeID 
                           select new { Technology = techTask.TechnologyMaster, Task = techTask.Task, Employee = employeeTask.Employee }).ToList();
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
     
       public void AssignEmployeeToProject(int employeeID, int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var project = (from emp in dc.EmployeeProjectMaps 
                           where emp.ProjectID == projectID 
                           select emp).First();
            project.EmployeeID =employeeID;
            dc.SubmitChanges();
        }
       public void  CreateTaskInProject(Task task, int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            ProjectTaskMap projectTask = new ProjectTaskMap();
            projectTask.ProjectID = projectID;
            projectTask.TaskID = task.TaskID;
            dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
            dc.SubmitChanges();
            
            
        }
        public void AssignTechnologyToTask(int technologyID, int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            
            var assignTech = (from task in dc.TechTaskMaps
                              where task.TaskID == taskID 
                              select task).First();
            assignTech.TechID = technologyID;
        }
        //***************************************************************************************************
        public void UpdateTechnologiesForTask(List<int> technologyIDs, int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var updateTechTask = (from techTask in dc.TechTaskMaps 
                                  where techTask.TaskID == taskID
                                  select techTask.TechID).ToList();

            updateTechTask = technologyIDs;
            dc.SubmitChanges();
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
        public void AddProject(Project project, int id, string projectName, int budget, int statusId, int clientId)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            project.ProjectID = id;
            project.ProjectName = projectName;
            project.ProjectBudget = budget;
            project.StatusID = statusId;
            project.ClientID = clientId;
            string checkColumn = validate.checkCompulsoryProjectColumn(project);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Projects.InsertOnSubmit(project);
            dc.SubmitChanges();
        }

        public void AddTechnology(TechnologyMaster technology, int id, string techName, int cost)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            technology.TechID = id;
            technology.TechName = techName;
            technology.TechCost = cost;
            string checkColumn = validate.checkCompulsoryTechnologyColumn(technology);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.TechnologyMasters.InsertOnSubmit(technology);
            dc.SubmitChanges();
        }

            public void AddDepartment(DepartmentMaster department, int id, string deptName, int companyid)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            department.DepartmentID = id;
            department.DepartmentName = deptName;
            department.CompanyID = companyid;
            string checkColumn =validate.checkCompulsoryDepartmentColumn(department);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.DepartmentMasters.InsertOnSubmit(department);
            dc.SubmitChanges();
        }
        public void AddClient(Client client, int id, string clientName, string Address, int companyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            client.ClientID = id;
            client.ClientName = clientName;
            client.ClientAddress = Address;
            client.CompanyID = companyID;
            string checkColumn = validate.checkCompulsoryClientColumn(client);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Clients.InsertOnSubmit(client);
            dc.SubmitChanges();
        }
        public void AddCompany(Company company, int id, string companyName, string Address)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            company.CompanyID = id;
            company.CompanyName = companyName;
            company.CompanyAddress = Address;
            string checkColumn = validate.checkCompulsoryCompanyColumn(company);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Companies.InsertOnSubmit(company);
            dc.SubmitChanges();
        }
        public void AddEmployee(Employee employee, int id, string empName, string Address, TimeSpan dateOfjoin, TimeSpan dateOfleave, int salary, int deptid)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            employee.EmployeeID = id;
            employee.EmployeeName = empName;
            employee.EmployeeAddress = Address;
            employee.EmployeeJoined = dateOfjoin;
            employee.EmployeeLeaved = dateOfleave;
            employee.EmployeeSalary = salary;
            employee.DepartmentID = deptid;


            string checkColumn = validate.checkCompulsoryEmployeeColumn(employee);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Employees.InsertOnSubmit(employee);
            dc.SubmitChanges();
        }
        public void AddTask(Task task, int id, string taskName, int statusID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            task.TaskID = id;
            task.TaskName = taskName;
            task.StatusID = statusID;
            string checkColumn = validate.checkCompulsoryTaskColumn(task);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }
            dc.Tasks.InsertOnSubmit(task);
            dc.SubmitChanges();
        }

        public void AddStatus(StatusMaster status, int id, string statusName)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            status.StatusID = id;
            status.StatusName = name;
            string checkColumn = validate.checkCompulsoryStatusColumn(status);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }

            dc.StatusMasters.InsertOnSubmit(status);
            dc.SubmitChanges();
        }
        public void AddEmployeeProjectMap(EmployeeProjectMap employeeProject, int id, int employeeID, int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            employeeProject.EmployeeProjectMapID = id;
            employeeProject.EmployeeID = employeeID;
            employeeProject.ProjectID = projectID;
            dc.EmployeeProjectMaps.InsertOnSubmit(employeeProject);
            dc.SubmitChanges();
        }
        public void AddEmployeeTaskMap(EmployeeTaskMap employeeTask, int ID, int employeeId, int taskId)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            employeeTask.EmployeeTaskMapID = ID;
            employeeTask.EmployeeID = employeeId;
            employeeTask.TaskID = taskId;
            dc.EmployeeTaskMaps.InsertOnSubmit(employeeTask);
            dc.SubmitChanges();
        }
        public void AddProjectTaskMap(ProjectTaskMap projectTask, int id, int projectId, int taskId)

        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            projectTask.ProjectTaskMapID = id;
            projectTask.ProjectID = projectId;
            projectTask.TaskID = taskId;
            dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
            dc.SubmitChanges();

        }
        public void AddTechProjectMap(TechProjectMap techProject, int Id, int techID, int projectId)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            techProject.TechProjectMapID = Id;
            techProject.TechID = techID;
            techProject.ProjectID = projectId;
            dc.TechProjectMaps.InsertOnSubmit(techProject);
            dc.SubmitChanges();

        }
        public void AddTechTaskMap(TechTaskMap techTask, int Id, int techId, int taskId)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            techTask.TechTaskMapID = Id;
            techTask.TechID = techId;
            techTask.TaskID = taskId;
        }



    }
}
