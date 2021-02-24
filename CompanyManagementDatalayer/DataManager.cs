using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Data.SqlClient;


namespace CompanyManagementDatalayer
{
    public class DataManager
    {
        static string connectionString = @"Data Source=LAPTOP-LT1J0C29\SQLEXPRESS;Initial Catalog=CompanyDB;Integrated Security=True";
       

        public void GetAllProjectList()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "select * from Project";
            SqlDataReader dataReader;
            SqlCommand cmd = new SqlCommand(query,conn);
            dataReader = cmd.ExecuteReader();
            Console.WriteLine(dataReader);
        }
        public List<Employee> GetAllEmployeeForProjects(int projectID)
        {
            List<Employee> employeeList = new List<Employee>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query ="select   emp.* from Employee emp inner join EmployeeProject empProject on emp.EmployeeID = empProject.EmployeeID where empProject.ProjectID = " + projectID;
            conn.Open();
            var command = new SqlCommand(query, conn);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.EmployeeID = Convert.ToInt32( dr["EmployeeID"]);
                emp.EmployeeName = dr["EmployeeName"].ToString();
                emp.EmployeeAddress = dr["EmployeeAddress"].ToString();
                emp.EmployeeSalary = Convert.ToInt32(dr["EmployeeSalary"]);
                emp.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                employeeList.Add(emp);

            }
            dr.Close();
            conn.Close();
            return employeeList;
        }
        public List<Project> GetAllProjectSForEmployee(int employeeID)
        {
            List<Project> projectList = new List<Project>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "select p.* from Project p inner join EmployeeProject ep on p.ProjectID = ep.ProjectID where ep.EmployeeID = " + employeeID;
            conn.Open();
            var command = new SqlCommand(query, conn);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Project project = new Project()
                {
                    ProjectID = Convert.ToInt32(dr["ProjectID"]),
                    ProjectName = dr["ProjectName"].ToString(),
                    ProjectBudget = Convert.ToInt32(dr["ProjectBudget"]),
                    ClientID = Convert.ToInt32(dr["ClientID"]),
                    StatusID = Convert.ToInt32(dr["StatusID"])

                };
                projectList.Add(project);
            }
            dr.Close();
            conn.Close();
            return projectList;
        }
        public List<Task> GetAllTasksForEmployee(int employeeID)
        {
            List<Task> taskList = new List<Task>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = " select t.*from Task t inner join EmployeeTaskMap et on t.TaskID = et.TaskID  where et.EmployeeID =  " + employeeID;
            conn.Open();
            var command = new SqlCommand(query, conn);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Task task = new Task()
                {
                    TaskID = Convert.ToInt32(dr["TaskID"]),
                    TaskName = dr["TaskName"].ToString(),
                    StatusID = Convert.ToInt32(dr["StatusID"])
                };
                taskList.Add(task);

            }
            dr.Close();
            conn.Close();
            return taskList;
        }
        public List<Project> GetAllProjects()
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Project> projectList = (from project in dc.Projects
                                             select project).ToList();
                return projectList;
                //Is This also Correct ...return dc.projects.tolist();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TechnologyMaster> GetAllTechologies()
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<TechnologyMaster> techList = (from tech in dc.TechnologyMasters
                                                   select tech).ToList();
                return techList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetEmployeeCountForProject(int projectID)
        {
            try
            {
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    var employeeList = (from emp in dc.EmployeeProjects
                                        where emp.ProjectID == projectID
                                        select emp.EmployeeID).ToList();
                    return employeeList.Count;
                }
                else
                {

                    throw new Exception(QueryResource.ProjectNotFound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<Employee> GetEmployeesForProject(int projectID)
        {
            try
            {
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Employee> employeeList = (from emp in dc.EmployeeProjects
                                                   where emp.ProjectID == projectID
                                                   select emp.Employee).ToList();

                    return employeeList;
                }
                else
                {
                    throw new Exception(QueryResource.ProjectNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Project> GetAllDelayedProjects()
        {
            try
            {

                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Project> delayedProjects = (from project in dc.Projects
                                                 where project.StatusID == (int)StatusEnum.Delayed
                                                 select project).ToList();

                return delayedProjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Project> GetAllProjectsForEmployee(int employeeID)
        {
            try
            {
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Project> projectsList = (from emp in dc.EmployeeProjects where emp.EmployeeID == employeeID select emp.Project).ToList();
                    return projectsList;
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Task> GetAllTasksForEmployee(int employeeID)
        {
            try
            {
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Task> taskList = (from emp in dc.EmployeeTaskMaps
                                           where emp.EmployeeID == employeeID
                                           select emp.Task).ToList();
                    return taskList;
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TechTaskMap> GetAllTechnologyTasksForEmployee(int technologyID, int employeeID)
        {
            /* try
             {
                 CompanyDBDataContext dc = new CompanyDBDataContext();
                 var techTaskList = (from employeeTask in dc.EmployeeTaskMaps
                                     join techTask in dc.TechTaskMaps on employeeTask.TaskID equals techTask.TaskID
                                     where employeeTask.EmployeeID == employeeID && techTask.TechID == technologyID
                                     select new { Technology = techTask.TechnologyMaster, Task = techTask.Task, Employee = employeeTask.Employee }).ToList();
                 return techTaskList.Select(tuple => (tuple.Technology, tuple.Task, tuple.Employee)).ToList();
             }
             catch (Exception ex)
             {
                 throw ex;
             }*/
            try
            {
                var techPresent = ValidationHelper.IfTechnologyExist(technologyID);
                var empPresent = ValidationHelper.IfEmployeeExist(employeeID);
                if (techPresent && empPresent)
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    var result = (from techTask in dc.TechTaskMaps join employeeTask in dc.EmployeeTaskMaps on techTask.TaskID equals employeeTask.TaskID where techTask.TechID == technologyID && employeeTask.EmployeeID == employeeID select techTask).ToList();
                    return result;
                }
                else if (!techPresent || !empPresent)
                {
                    if (techPresent)
                    {
                        throw new Exception(QueryResource.EmployeeNotFound);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechnologyDoesNotExist);
                    }
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound + QueryResource.TechnologyDoesNotExist);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Project> GetAllTechnologyProjects(int technologyID)
        {
            try
            {
                if (ValidationHelper.IfTechnologyExist(technologyID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Project> projectList = (from tech in dc.TechProjectMaps
                                                 where tech.TechID == technologyID
                                                 select tech.Project).ToList();
                    return projectList;
                }
                else
                {
                    throw new Exception(QueryResource.TechnologyDoesNotExist);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Task> GetAllActiveTasksForProject(int projectID)
        {
            try
            {
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Task> taskList = (from project in dc.ProjectTaskMaps
                                           where project.ProjectID == projectID && project.Task.StatusID == (int)StatusEnum.Active
                                           select project.Task).ToList();

                    return taskList;
                }
                else
                {
                    throw new Exception(QueryResource.ProjectNotFound);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<TechnologyMaster> GetAllTechnologiesForEmployee(int employeeID)
        {
            /*  try
              {
                  CompanyDBDataContext dc = new CompanyDBDataContext();
                  var techList = (from employeeTask in dc.EmployeeTaskMaps
                                  join techTask in dc.TechTaskMaps on employeeTask.TaskID equals techTask.TaskID
                                  where employeeTask.EmployeeID == employeeID
                                  select new { Technology = techTask.TechnologyMaster, Task = techTask.Task, Employee = employeeTask.Employee }).ToList();
                  return techList.Select(tuple => (tuple.Technology, tuple.Employee)).ToList();
              }
              catch (Exception ex)
              {
                  throw ex;
              }*/
            try
            {
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    var techList = (from employeeTask in dc.EmployeeProjects join techTask in dc.TechProjectMaps on employeeTask.ProjectID equals techTask.ProjectID where employeeTask.EmployeeID == employeeID select techTask.TechnologyMaster).ToList();
                    return techList;
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetProjectCountForEmployee(int employeeID)
        {
            try
            {
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<Project> projectList = (from emp in dc.EmployeeProjects
                                                 where emp.EmployeeID == employeeID
                                                 select emp.Project).ToList();

                    return projectList.Count;
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Project> GetAllActiveProjectsManagedByEmployee(int employeeID)
        {
            try
            {
                if (ValidationHelper.IsManager(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Project> activeProjectList = (from emp in dc.EmployeeProjects where emp.EmployeeID == employeeID && emp.Project.StatusID == (int)StatusEnum.Active select emp.Project).ToList();
                    return activeProjectList;
                }
                else
                {
                    throw new Exception(QueryResource.NonManager);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public List<Task> GetAllDelayedTasksForEmployee(int employeeID)
        {
            try
            {
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Task> delayedTaskList = (from emp in dc.EmployeeTaskMaps where emp.EmployeeID == employeeID && emp.Task.StatusID == (int)StatusEnum.Delayed select emp.Task).ToList();
                    return delayedTaskList;
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public void AssignEmployeeToProject(int employeeID, int projectID)
        {
            try
            {
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    var project = (from emp in dc.EmployeeProjects
                                   where emp.ProjectID == projectID
                                   select emp).First();
                    if (ValidationHelper.IfEmployeeExist(employeeID))
                    {
                        project.EmployeeID = employeeID;
                        dc.SubmitChanges();
                    }
                    else { throw new Exception(QueryResource.EmployeeNotFound); }
                }
                else
                {
                    throw new Exception(QueryResource.ProjectNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateTaskInProject(Task task, int projectID)
        {
            try
            {
                bool taskPresent = ValidationHelper.IfTaskExist(task.TaskID);
                bool projectPresent = ValidationHelper.IfProjectExist(projectID);
                if (projectPresent && taskPresent)
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    ProjectTaskMap projectTask = new ProjectTaskMap();
                    projectTask.TaskID = task.TaskID;
                    projectTask.ProjectID = projectID;
                    dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
                    dc.SubmitChanges();
                }
                else if (!taskPresent || !projectPresent)
                {
                    if (taskPresent)
                    {
                        throw new Exception(QueryResource.TechnologyDoesNotExist);
                    }
                    else if (projectPresent)
                    {
                        throw new Exception(QueryResource.TaskDoesNotExist);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechAndTaskDoesNotExist);
                    }
                }
            }
            catch (Exception ex) { throw ex; }


        }
        public void AssignTechnologyToTask(int technologyID, int taskID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                bool taskPresent = ValidationHelper.IfTaskExist(taskID);
                bool techPresent = ValidationHelper.IfTechnologyExist(technologyID);
                if (taskPresent && techPresent)
                {

                    TechTaskMap tech = new TechTaskMap();
                    tech.TaskID = taskID;
                    tech.TechID = technologyID;
                    dc.TechTaskMaps.InsertOnSubmit(tech);
                    dc.SubmitChanges();



                }
                else if (!taskPresent || !techPresent)
                {
                    if (taskPresent)
                    {
                        throw new Exception(QueryResource.TechnologyDoesNotExist);
                    }
                    else if (techPresent)
                    {
                        throw new Exception(QueryResource.TaskDoesNotExist);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechAndTaskDoesNotExist);
                    }
                }

            }
            catch (Exception ex) { throw ex; }
        }
        public void UpdateTechnologiesForTask(List<int> technologyIDs, int taskID)
        {
            try
            {

                CompanyDBDataContext dc = new CompanyDBDataContext();
                bool taskPresent = ValidationHelper.IfTaskExist(taskID);

                if (taskPresent)
                {
                    List<TechTaskMap> result = (from techtask in dc.TechTaskMaps where techtask.TaskID == taskID select techtask).ToList();
                    dc.TechTaskMaps.DeleteAllOnSubmit(result);
                    dc.SubmitChanges();



                    foreach (int techID in technologyIDs)
                    {
                        bool techPresent = ValidationHelper.IfTechnologyExist(techID);
                        if (techPresent)
                        {
                            TechTaskMap techTask = new TechTaskMap();
                           
                            techTask.TaskID = taskID;
                            techTask.TechID = techID;
                            dc.TechTaskMaps.InsertOnSubmit(techTask);



                        }
                        else
                        {
                            throw new Exception(QueryResource.TechnologyDoesNotExist);
                        }
                    }
                    dc.SubmitChanges();
                }
                else
                {
                    throw new Exception(QueryResource.TaskDoesNotExist);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteEmployeeFromSystem(int employeeID)
        {
            try
            {
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<EmployeeProject> employeeProjectList = (from empProject in dc.EmployeeProjects where empProject.EmployeeID == employeeID select empProject).ToList();

                    List<EmployeeTaskMap> employeeTaskList = (from employeeTask in dc.EmployeeTaskMaps where employeeTask.EmployeeID == employeeID select employeeTask).ToList();
                    Employee employeeToDelete = (from employee in dc.Employees where employee.EmployeeID == employeeID select employee).First();
                    dc.EmployeeTaskMaps.DeleteAllOnSubmit(employeeTaskList);
                    dc.EmployeeProjects.DeleteAllOnSubmit(employeeProjectList);
                    dc.Employees.DeleteOnSubmit(employeeToDelete);
                    dc.SubmitChanges();
                    Console.WriteLine("Employee Deleted Successfully");
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTechnology(int technology)
        {
            try
            {
                if (ValidationHelper.IfTechnologyExist(technology))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<TechProjectMap> techProjectList = (from techProject in dc.TechProjectMaps where techProject.TechID == technology select techProject).ToList();

                    List<TechTaskMap> techTaskList = (from techTask in dc.TechTaskMaps where techTask.TechID == technology select techTask).ToList();
                    TechnologyMaster technologyToDelete = (from tech in dc.TechnologyMasters where tech.TechID == technology select tech).First();
                    dc.TechTaskMaps.DeleteAllOnSubmit(techTaskList);
                    dc.TechProjectMaps.DeleteAllOnSubmit(techProjectList);
                    dc.TechnologyMasters.DeleteOnSubmit(technologyToDelete);
                    dc.SubmitChanges();
                    Console.WriteLine("Technology Deleted Successfully"); ;
                }
                else { throw new Exception(QueryResource.TechnologyDoesNotExist); }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public void DeleteTask(int taskID)
        {
            try
            {
                if (ValidationHelper.IfTaskExist(taskID))
                {

                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<ProjectTaskMap> taskProjectList = (from taskProject in dc.ProjectTaskMaps where taskProject.TaskID == taskID select taskProject).ToList();

                    List<EmployeeTaskMap> employeeTaskList = (from empTask in dc.EmployeeTaskMaps where empTask.TaskID == taskID select empTask).ToList();
                    Task taskToDelete = (from task in dc.Tasks where task.TaskID == taskID select task).First();
                    dc.ProjectTaskMaps.DeleteAllOnSubmit(taskProjectList);
                    dc.EmployeeTaskMaps.DeleteAllOnSubmit(employeeTaskList);
                    dc.Tasks.DeleteOnSubmit(taskToDelete);
                    dc.SubmitChanges();
                    Console.WriteLine("Task Deleted Successfully"); ;
                }
                else
                {
                    throw new Exception(QueryResource.TaskDoesNotExist);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteProject(int projectID)
        {
            try
            {
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<ProjectTaskMap> taskProjectList = (from taskProject in dc.ProjectTaskMaps where taskProject.ProjectID == projectID select taskProject).ToList();

                    List<EmployeeProject> employeeProjectList = (from empProject in dc.EmployeeProjects where empProject.ProjectID == projectID select empProject).ToList();
                    Project projectToDelete = (from project in dc.Projects where project.ProjectID == projectID select project).First();
                    dc.ProjectTaskMaps.DeleteAllOnSubmit(taskProjectList);
                    dc.EmployeeProjects.DeleteAllOnSubmit(employeeProjectList);
                    dc.Projects.DeleteOnSubmit(projectToDelete);
                    dc.SubmitChanges();
                    Console.WriteLine("Project Deleted Successfully");
                }
                else
                {
                    throw new Exception(QueryResource.ProjectNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddProject(Project project)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryProjectColumn(project);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            else
            {
                dc.Projects.InsertOnSubmit(project);
                dc.SubmitChanges();
            }
        }
        public void AddTechnology(TechnologyMaster technology)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            string dataValidate = ValidationHelper.CheckCompulsoryTechnologyColumn(technology);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);

            }
            else
            {
                dc.TechnologyMasters.InsertOnSubmit(technology);
                dc.SubmitChanges();
            }
        }
        public void AddDepartment(DepartmentMaster department)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            string dataValidate = ValidationHelper.CheckCompulsoryDepartmentColumn(department);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);

            }
            else
            {
                dc.DepartmentMasters.InsertOnSubmit(department);
                dc.SubmitChanges();
            }
        }
        public void AddClient(Client client)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            string dataValidate = ValidationHelper.CheckCompulsoryClientColumn(client);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);

            }
            else
            {
                dc.Clients.InsertOnSubmit(client);
                dc.SubmitChanges();
            }
        }
        public void AddCompany(Company company)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryCompanyColumn(company);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);

            }
            else
            {
                dc.Companies.InsertOnSubmit(company);
                dc.SubmitChanges();
            }
        }
        public void AddEmployee(Employee employee)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryEmployeeColumn(employee);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);

            }
            else
            {
                dc.Employees.InsertOnSubmit(employee);
                dc.SubmitChanges();
                Console.WriteLine("Employee Added");
            }

        }
        public void AddTask(Task task)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryTaskColumn(task);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            else
            {
                dc.Tasks.InsertOnSubmit(task);
                dc.SubmitChanges();
            }
        }
        public void AddStatus(StatusMaster status)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            string dataValidate = ValidationHelper.CheckCompulsoryStatusColumn(status);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            else
            {
                dc.StatusMasters.InsertOnSubmit(status);
                dc.SubmitChanges();
            }
        }
        public void AddEmployeeProjectMap(EmployeeProject employeeProject)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryEmployeeProjectColumn(employeeProject);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            else
            {
                dc.EmployeeProjects.InsertOnSubmit(employeeProject);
                dc.SubmitChanges();
            }
        }
        public void AddEmployeeTaskMap(EmployeeTaskMap employeeTask)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryEmployeeTaskColumn(employeeTask);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            else
            {
                dc.EmployeeTaskMaps.InsertOnSubmit(employeeTask);
                dc.SubmitChanges();
            }
        }
        public void AddProjectTaskMap(ProjectTaskMap projectTask)

        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryProjectTaskColumn(projectTask);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
            dc.SubmitChanges();

        }
        public void AddTechProjectMap(TechProjectMap techProject)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryTechProjectColumn(techProject);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            dc.TechProjectMaps.InsertOnSubmit(techProject);
            dc.SubmitChanges();

        }
        public void AddTechTaskMap(TechTaskMap techTask)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string dataValidate = ValidationHelper.CheckCompulsoryTechtaskColumn(techTask);
            if (dataValidate != QueryResource.AllFieldsPresent)
            {
                throw new Exception(dataValidate);
            }
            dc.TechTaskMaps.InsertOnSubmit(techTask);
            dc.SubmitChanges();
        }

        public int GetProjectCountOfTechnology(int technologyID)
        {
            try
            {
                if (ValidationHelper.IfTechnologyExist(technologyID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    int projectList = (from techProject in dc.TechProjectMaps
                                       where techProject.TechID == technologyID
                                       select techProject.Project).ToList().Count;
                    return projectList;
                }
                else
                {
                    throw new Exception(QueryResource.TechnologyDoesNotExist);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TechnologyMaster> CheckTechnologyForProject(int projectID)
        {
            try
            {
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<TechnologyMaster> techProject = (from project in dc.TechProjectMaps where project.ProjectID == projectID select project.TechnologyMaster).ToList();
                    return techProject;
                }
                else
                {
                    throw new Exception(QueryResource.ProjectNotFound);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetTechnologyCountForTask(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<int> techTask = (from task in dc.TechTaskMaps where task.TaskID == taskID select task.TechID).ToList();
            return techTask.Count;
        }
        public int GetStatusOfTask(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            int taskStatus = (from task in dc.Tasks where task.TaskID == taskID select task.StatusID).First();
            return taskStatus;
        }
        public int GetStatusOfProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            int projectStatus = (from project in dc.Projects where project.ProjectID == projectID select project.StatusID).First();
            return projectStatus;

        }
        public void UpdateStatusOfProject(int projectID, int statusID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Project updateStatus = (from project in dc.Projects where project.ProjectID == projectID select project).First();
            updateStatus.StatusID = statusID;
            try
            {
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /* public void DeleteEmployee(int empID)
         {
            try {
                 if (ValidationHelper.IfEmployeeExist(empID))
                 {
                     CompanyDBDataContext dc = new CompanyDBDataContext();

                     var deleteEmployee = (from emp in dc.Employees
                                           join empProject in dc.EmployeeProjects
                                           on emp.EmployeeID equals empProject.EmployeeID
                                           join empTask in dc.EmployeeTaskMaps
                                           on emp.EmployeeID equals empTask.EmployeeID
                                           where emp.EmployeeID == empID
                                           select emp).ToList();
                     foreach (var employee in deleteEmployee)
                     {
                         dc.Employees.DeleteOnSubmit(employee);
                     }
                 }
             }catch(Exception ex)
             {
                 throw ex;
             }

         }*/

        public List<Project> GEtProjectListOfTask(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var projectList = (from project in dc.ProjectTaskMaps where project.TaskID == taskID select project.Project).ToList();
            return projectList;
        }

        public int GetProjectStatus(int projectID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                int projectStatus = (from project in dc.Projects where project.ProjectID == projectID select project.StatusID).First();
                return projectStatus;
            }
            catch (Exception ex) { throw ex; }
        }
        public DepartmentMaster GetDepartment(int departmentID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            DepartmentMaster departmentObj = (from dept in dc.DepartmentMasters where dept.DepartmentID == departmentID select dept).First();
            return departmentObj;

        }
        public StatusMaster GetStatus(int statusID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            StatusMaster statusObj = (from status in dc.StatusMasters where status.StatusID == statusID select status).First();
            return statusObj;

        }
        public Company GetCompany(int companyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Company companyObj = (from company in dc.Companies where company.CompanyID == companyID select company).First();
            return companyObj;

        }
        public Client GetClient(int clientID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Client clientObj = (from client in dc.Clients where client.ClientID == clientID select client).First();
            return clientObj ;

        }
    }
}
