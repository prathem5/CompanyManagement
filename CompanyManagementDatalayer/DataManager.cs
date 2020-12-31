using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CompanyManagementDatalayer
{
    public class DataManager
    {
        DataManager dm = new DataManager();
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
                }else
                {
                    throw new Exception(QueryResource.ProjectIDNotFound);
                }
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
                if (ValidationHelper.IfProjectExist(projectID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<Employee> employeeList = (from emp in dc.EmployeeProjects
                                                   where emp.ProjectID == projectID
                                                   select emp.Employee).ToList();

                    return employeeList;
                }else
                {
                    throw new Exception(QueryResource.ProjectIDNotFound);
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
                }else if(!techPresent || !empPresent)
                {
                    if (techPresent)
                    {
                        throw new Exception(QueryResource.EmployeeNotFound);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechnologyNotexist);
                    }
                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotFound + QueryResource.TechnologyNotexist);
                }
            }catch(Exception ex)
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
                } else 
                { throw new Exception(QueryResource.TechnologyNotexist);
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
                    throw new Exception(QueryResource.ProjectIDNotFound);
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
                }else
                {
                    throw new Exception(QueryResource.EmployeeNotFound);
                }
            }catch(Exception ex)
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
                }else
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
            catch (Exception ex) { throw ex; }
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
                    }else { throw new Exception(QueryResource.EmployeeNotFound); }
                }
                else
                {
                    throw new Exception(QueryResource.ProjectIDNotFound);
                }
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
                var taskPresent = ValidationHelper.IfTaskExist(taskID);
                var projectPresent = ValidationHelper.IfProjectExist(projectID);
                if ( projectPresent&& taskPresent)
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    ProjectTaskMap projectTask = new ProjectTaskMap
                    {
                        ProjectID = projectID,
                        TaskID = taskID
                    };
                    dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
                    dc.SubmitChanges();
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
                    var techAssign = (from techTask in dc.TechTaskMaps where techTask.TaskID == taskID select techTask).ToList();
                    foreach (TechTaskMap tech in techAssign)
                    {
                        tech.TechID = technologyID;
                        dc.TechTaskMaps.InsertOnSubmit(tech);
                    }
                    dc.SubmitChanges();
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
                   foreach(TechTaskMap tech in result)
                    {
                        dc.TechTaskMaps.DeleteOnSubmit(tech);
                    }
                    dc.SubmitChanges();
                }
                else
                {
                    throw new Exception(QueryResource.TaskNotExist);
                }
               
                foreach( int techID in technologyIDs)
                {
                    bool techPresent = ValidationHelper.IfTechnologyExist(techID);
                    if (techPresent)
                    {
                        DataManager dm = new DataManager();
                        dm.AssignTechnologyToTask(techID, taskID);

                    }
                    else
                    {
                        throw new Exception(QueryResource.TechnologyNotexist);
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
                if (ValidationHelper.IfEmployeeExist(employeeID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    var deleteEmployee = (from emp in dc.Employees
                                          join empProject in dc.EmployeeProjects 
                                          on emp.EmployeeID equals empProject.EmployeeID 
                                          join empTask in dc.EmployeeTaskMaps
                                          on emp.EmployeeID equals empTask.EmployeeID
                                          where emp.EmployeeID == employeeID 
                                          select emp).ToList();
               foreach( var employee in deleteEmployee)
                    {
                        dc.Employees.DeleteOnSubmit(employee);
                    }
                    dc.SubmitChanges();
                }
              
            }
            catch (Exception ex) { throw ex; }
        }
        public void DeleteTechnology(int technology)
        {
            try
            {
                if (ValidationHelper.IfTechnologyExist(technology))
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
                else { throw new Exception(QueryResource.TechnologyNotexist); }
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
                    List<Task> deleteTask = (from task in dc.Tasks
                                             where task.TaskID == taskID
                                             select task).ToList();
                    foreach (Task task in deleteTask)
                    {
                        dc.Tasks.DeleteOnSubmit(task);

                    }
                    dc.SubmitChanges();
                }
                else
                {
                    throw new Exception(QueryResource.TaskNotExist);
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
                    List<Project> deleteProject = (from project in dc.Projects
                                                   where project.ProjectID == projectID
                                                   select project).ToList();
                    foreach (Project project in deleteProject)
                    {
                        dc.Projects.DeleteOnSubmit(project);

                    }
                    dc.SubmitChanges();
                }
                else
                {
                    throw new Exception(QueryResource.ProjectIDNotFound);
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
            string checkColumn = ValidationHelper.CheckCompulsoryProjectColumn(project);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }
            dc.Projects.InsertOnSubmit(project);
            dc.SubmitChanges();
        }
        public void AddTechnology(TechnologyMaster technology)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            
            string checkColumn = ValidationHelper.CheckCompulsoryTechnologyColumn(technology);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.TechnologyMasters.InsertOnSubmit(technology);
            dc.SubmitChanges();
        }
        public void AddDepartment(DepartmentMaster department)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
           
            string checkColumn = ValidationHelper.CheckCompulsoryDepartmentColumn(department);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.DepartmentMasters.InsertOnSubmit(department);
            dc.SubmitChanges();
        }
        public void AddClient(Client client)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
           
            string checkColumn = ValidationHelper.CheckCompulsoryClientColumn(client);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Clients.InsertOnSubmit(client);
            dc.SubmitChanges();
        }
        public void AddCompany(Company company)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string checkColumn = ValidationHelper.CheckCompulsoryCompanyColumn(company);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Companies.InsertOnSubmit(company);
            dc.SubmitChanges();
        }
        public void AddEmployee(Employee employee)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string checkColumn = ValidationHelper.CheckCompulsoryEmployeeColumn(employee);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Employees.InsertOnSubmit(employee);
            dc.SubmitChanges();
        }
        public void AddTask(Task task)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            string checkColumn = ValidationHelper.CheckCompulsoryTaskColumn(task);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }
            dc.Tasks.InsertOnSubmit(task);
            dc.SubmitChanges();
        }
        public void AddStatus(StatusMaster status)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            
            string checkColumn = ValidationHelper.CheckCompulsoryStatusColumn(status);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }

            dc.StatusMasters.InsertOnSubmit(status);
            dc.SubmitChanges();
        }
        public void AddEmployeeProjectMap(EmployeeProject employeeProject)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
           
            dc.EmployeeProjects.InsertOnSubmit(employeeProject);
            dc.SubmitChanges();
        }
        public void AddEmployeeTaskMap(EmployeeTaskMap employeeTask)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            
            dc.EmployeeTaskMaps.InsertOnSubmit(employeeTask);
            dc.SubmitChanges();
        }
        public void AddProjectTaskMap(ProjectTaskMap projectTask)

        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
           
            dc.ProjectTaskMaps.InsertOnSubmit(projectTask);
            dc.SubmitChanges();

        }
        public void AddTechProjectMap(TechProjectMap techProject)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
           
            dc.TechProjectMaps.InsertOnSubmit(techProject);
            dc.SubmitChanges();

        }
        public void AddTechTaskMap(TechTaskMap techTask)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            dc.TechTaskMaps.InsertOnSubmit(techTask);
            dc.SubmitChanges();
        }
        
        public int  GetAllProjectOfTechnology(int technologyID)
        {
            try
            {
                if (ValidationHelper.IfTechnologyExist(technologyID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    List<int> projectList = (from techProject in dc.TechProjectMaps
                                                 where techProject.TechID == technologyID
                                                 select techProject.ProjectID).ToList();
                    return projectList.Count;
                }
                else
                {
                    throw new Exception(QueryResource.TechnologyNotexist);
                }
            }catch(Exception ex) 
            {
                throw ex; 
            }
        }
        public List<TechnologyMaster> CheckTechnologyForPRoject(int projectID)
        {
            try
            {
                if(ValidationHelper.IfProjectExist(projectID)){
                    CompanyDBDataContext dc = new CompanyDBDataContext();

                    List<TechnologyMaster> techProject = (from peoject in dc.TechProjectMaps where peoject.ProjectID == projectID select peoject.TechnologyMaster).ToList();
                    return techProject;
                }
                else
                {
                    throw new Exception(QueryResource.ProjectIDNotFound);
                }

            }catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetAllTechnologyForTask(int taskID)
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
        


       

    }
}
