using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CompanyManagementDatalayer
{
    public class DataManger
    {
        ValidationHelper ValidationHelper = new ValidationHelper();
        public List<Project> GetAllProjects()
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Project> projectList = (from project in dc.Projects
                                             select project).ToList();
                return projectList.ToList();
                //Is This also Correct ...return dc.projects.tolist();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TechnologyMaster> getAllTechologies()
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                var employeeList = (from emp in dc.EmployeeProjects
                                             where emp.ProjectID == projectID
                                             select emp.EmployeeID).ToList();
                return employeeList.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Employee> GetEmployeesForProject(int projectID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Employee> employeeList = (from emp in dc.EmployeeProjects
                                              where emp.ProjectID == projectID
                                              select emp.Employee).ToList();

                return employeeList;
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Project> projectsList = (from emp in dc.EmployeeProjects where emp.EmployeeID == employeeID select emp.Project).ToList();
                return projectsList;
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Task> taskList = (from emp in dc.EmployeeTaskMaps
                                       where emp.EmployeeID == employeeID
                                       select emp.Task).ToList();
                return taskList;
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
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var result = (from t in dc.TechTaskMaps join e in dc.EmployeeTaskMaps on t.TaskID equals e.TaskID where t.TechID == technologyID && e.EmployeeID == employeeID select t).ToList();
            return result;
        }
        public List<Project> GetAllTechnologyProjects(int technologyID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Project> projectList = (from tech in dc.TechProjectMaps
                                             where tech.TechID == technologyID
                                             select tech.Project).ToList();
                return projectList;
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Task> taskList = (from project in dc.ProjectTaskMaps
                                       where project.ProjectID == projectID && project.Task.StatusID == (int)StatusEnum.Active
                                       select project.Task).ToList();

                return taskList;
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
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var techList = (from e in dc.EmployeeProjects join t in dc.TechProjectMaps on e.ProjectID equals t.ProjectID where e.EmployeeID == employeeID select t.TechnologyMaster).ToList();
            return techList;

        }
        public int GetProjectCountForEmployee(int employeeID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                
                List<Project> projectList = (from emp in dc.EmployeeProjects
                                             where emp.EmployeeID == employeeID
                                             select emp.Project).ToList();

                return projectList.Count;
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
                if (ValidationHelper.isManager(employeeID))
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
            catch (Exception ex) { throw ex; }
        }
        public List<Task> GetAllDelayedTasksForEmployee(int employeeID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Task> delayedTaskList = (from emp in dc.EmployeeTaskMaps where emp.EmployeeID == employeeID && emp.Task.StatusID == (int)StatusEnum.Delayed select emp.Task).ToList();
                return delayedTaskList;
            }
            catch (Exception ex) { throw ex; }
        }
        public void AssignEmployeeToProject(int employeeID, int projectID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                var project = (from emp in dc.EmployeeProjects
                               where emp.ProjectID == projectID
                               select emp).First();
                project.EmployeeID = employeeID;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateTaskInProject(int taskID, int projectID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                ProjectTaskMap projectTask = new ProjectTaskMap();
                projectTask.ProjectID = projectID;
                projectTask.TaskID = taskID;
                dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
                dc.SubmitChanges();
            }
            catch (Exception ex) { throw ex; }


        }
        public void AssignTechnologyToTask(int technologyID, int taskID)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                bool taskPresent = ValidationHelper.IfTaskExist(taskID);
                bool techPresent = ValidationHelper.IftechnologyExist(technologyID);
                if (taskPresent && techPresent)
                {
                    TechTaskMap techTask = new TechTaskMap();
                    AddTechTaskMap(techTask, 1, technologyID, taskID);
                }
               else if(!taskPresent || !techPresent)
                {
                    if (taskPresent)
                    {
                        throw new Exception(QueryResource.TechnologyNotexist);
                    }else if (techPresent)
                    {
                        throw new Exception(QueryResource.TaskNotExist);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechAndTaskNotExit);
                    }
                }

            }
            catch (Exception ex) { throw ex; }
        }
        public void UpdateTechnologiesForTask(List<int> technologyIDs, int taskID)
        {
            try
            {
                var i = 0;
                CompanyDBDataContext dc = new CompanyDBDataContext();
                bool taskPresent = ValidationHelper.IfTaskExist(taskID);
                bool techPresent = ValidationHelper.IftechnologyExist(technologyIDs[i]);
                if (taskPresent && techPresent)
                {
                    var result = (from t in dc.TechTaskMaps where t.TaskID == taskID select t).ToList();
                   
                    foreach (var r in result)
                    {
                        r.TechID = technologyIDs[i];
                        i = i + 1;
                    }
                }
                else if (!taskPresent || !techPresent)
                {
                    if (taskPresent)
                    {
                        throw new Exception(QueryResource.TechnologyNotexist);
                    }
                    else if (techPresent)
                    {
                        throw new Exception(QueryResource.TaskNotExist);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechAndTaskNotExit);
                    }
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                
                
                    dc.Employees.DeleteOnSubmit(e);
                
                dc.SubmitChanges();
            }
            catch (Exception ex) { throw ex; }
        }
        public void DeleteTechnology(int technology)
        {
            try
            {
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<TechnologyMaster> deleteTechnology = (from tech in dc.TechnologyMasters
                                                           where tech.TechID == technology
                                                           select tech).ToList();
                foreach (TechnologyMaster tech in deleteTechnology)
                {
                    dc.TechnologyMasters.DeleteOnSubmit(tech);
                }
                dc.SubmitChanges();
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Task> deleteTask = (from task in dc.Tasks
                                         where task.TaskID == taskID
                                         select task).ToList();
                foreach (Task task in deleteTask)
                {
                    dc.Tasks.DeleteOnSubmit(task);

                }
                dc.SubmitChanges();
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
                CompanyDBDataContext dc = new CompanyDBDataContext();
                List<Project> deleteProject = (from project in dc.Projects
                                               where project.ProjectID == projectID
                                               select project).ToList();
                foreach (Project project in deleteProject)
                {
                    dc.Projects.DeleteOnSubmit(project);

                }
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddProject(Project project, int id, string projectName, int budget, int statusId, int clientId)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            project.ProjectID = id;
            project.ProjectName = projectName;
            project.ProjectBudget = budget;
            project.StatusID = statusId;
            project.ClientID = clientId;
            string checkColumn = ValidationHelper.checkCompulsoryProjectColumn(project);
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
            string checkColumn = ValidationHelper.checkCompulsoryTechnologyColumn(technology);
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
            string checkColumn = ValidationHelper.checkCompulsoryDepartmentColumn(department);
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
            string checkColumn = ValidationHelper.checkCompulsoryClientColumn(client);
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
            string checkColumn = ValidationHelper.checkCompulsoryCompanyColumn(company);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Companies.InsertOnSubmit(company);
            dc.SubmitChanges();
        }
        public void AddEmployee(Employee employee, int id, string empName, string Address, TimeSpan dateOfjoin, TimeSpan dateOfleave, int salary, int deptid, int departmentHeadId)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            employee.Employee1.EmployeeID = id;
            employee.EmployeeName = empName;
            employee.EmployeeAddress = Address;
            employee.EmployeeJoined = dateOfjoin;
            employee.EmployeeLeaved = dateOfleave;
            employee.EmployeeSalary = salary;
            employee.DepartmentID = deptid;
            employee.Employee2.EmployeeID = departmentHeadId;


            string checkColumn = ValidationHelper.checkCompulsoryEmployeeColumn(employee);
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
            string checkColumn = ValidationHelper.checkCompulsoryTaskColumn(task);
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
            status.StatusName = statusName;
            string checkColumn = ValidationHelper.checkCompulsoryStatusColumn(status);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }

            dc.StatusMasters.InsertOnSubmit(status);
            dc.SubmitChanges();
        }
        public void AddEmployeeProjectMap(EmployeeProject employeeProject, int id, int employeeID, int projectID ,int roleID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            employeeProject.EmployeeProjectMapID = id;
            employeeProject.EmployeeID = employeeID;
            employeeProject.ProjectID = projectID;
            employeeProject.RoleID = roleID;
            dc.EmployeeProjects.InsertOnSubmit(employeeProject);
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
        //************************************************************************************************************
        public int getAllProjectOfTechnology(int technologyID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            List<Project> projectList = (from techProject in dc.TechProjectMaps
                                         where techProject.TechID == technologyID
                                         select techProject.Project).ToList();
            return projectList.Count;
        }
        public void checkTechnologyForPRoject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();

            var techProject = (from peoject in dc.TechProjectMaps where peoject.ProjectID == projectID select peoject.TechID).ToList();

        }
        public int getAllTechnologyForTask(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var techTask = (from task in dc.TechTaskMaps where task.TaskID == taskID select task.TechID).ToList();
            return techTask.Count;
        }
        public int getStatusOfTask(int taskID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            var taskStatus = (from task in dc.Tasks where task.TaskID == taskID select task.StatusID).First();
            return taskStatus;
        }
        public int GetStatusOfProject(int projectID)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            int projectStatus = (from project in dc.Projects where project.ProjectID == projectID select project.StatusID).First();
            return projectStatus;

        }
        


       

    }
}
