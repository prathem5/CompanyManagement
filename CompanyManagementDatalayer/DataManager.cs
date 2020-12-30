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
                    TechTaskMap techTask = new TechTaskMap();
                    AddTechTaskMap(techTask);
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
                bool techPresent = ValidationHelper.IfTechnologyExist(technologyIDs[i]);
                if (taskPresent && techPresent)
                {
                    var result = (from techTask in dc.TechTaskMaps where techTask.TaskID == taskID select techTask).ToList();
                   
                    foreach (var record in result)
                    {
                        record.TechID = technologyIDs[i];
                        i =+ 1;
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
            Project project1 = new Project
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                ProjectBudget = project.ProjectBudget,
                StatusID = project.StatusID,
                ClientID = project.ClientID
            };
            string checkColumn = ValidationHelper.CheckCompulsoryProjectColumn(project1);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Projects.InsertOnSubmit(project1);
            dc.SubmitChanges();
        }
        public void AddTechnology(TechnologyMaster technology)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            TechnologyMaster technologyMaster = new TechnologyMaster
            {
                TechID = technology.TechID,
                TechName = technology.TechName,
                TechCost = technology.TechCost
            };
            string checkColumn = ValidationHelper.CheckCompulsoryTechnologyColumn(technologyMaster);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.TechnologyMasters.InsertOnSubmit(technologyMaster);
            dc.SubmitChanges();
        }
        public void AddDepartment(DepartmentMaster department)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            DepartmentMaster departmentMaster = new DepartmentMaster
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                CompanyID = department.CompanyID
            };
            string checkColumn = ValidationHelper.CheckCompulsoryDepartmentColumn(departmentMaster);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.DepartmentMasters.InsertOnSubmit(departmentMaster);
            dc.SubmitChanges();
        }
        public void AddClient(Client client)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Client client1 = new Client
            {
                ClientID = client.ClientID,
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                CompanyID = client.CompanyID
            };
            string checkColumn = ValidationHelper.CheckCompulsoryClientColumn(client1);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Clients.InsertOnSubmit(client1);
            dc.SubmitChanges();
        }
        public void AddCompany(Company company)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Company company1 = new Company
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
                CompanyAddress = company.CompanyAddress
            };
            string checkColumn = ValidationHelper.CheckCompulsoryCompanyColumn(company1);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Companies.InsertOnSubmit(company1);
            dc.SubmitChanges();
        }
        public void AddEmployee(Employee employee)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Employee employee1 = new Employee();
            employee1.Employee1.EmployeeID = employee.Employee1.EmployeeID;
            employee1.EmployeeName = employee.EmployeeName;
            employee1.EmployeeAddress = employee.EmployeeAddress;
            employee1.EmployeeJoined = employee.EmployeeJoined;
            employee1.EmployeeLeaved = employee.EmployeeLeaved;
            employee1.EmployeeSalary = employee.EmployeeSalary;
            employee1.DepartmentID = employee.DepartmentID;
            employee1.Employee2.EmployeeID = employee.Employee2.EmployeeID;


            string checkColumn = ValidationHelper.CheckCompulsoryEmployeeColumn(employee1);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);

            }
            dc.Employees.InsertOnSubmit(employee1);
            dc.SubmitChanges();
        }
        public void AddTask(Task task)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            Task task1 = new Task
            {
                TaskID = task.TaskID,
                TaskName = task.TaskName,
                StatusID = task.StatusID
            };
            string checkColumn = ValidationHelper.CheckCompulsoryTaskColumn(task1);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }
            dc.Tasks.InsertOnSubmit(task1);
            dc.SubmitChanges();
        }
        public void AddStatus(StatusMaster status)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            StatusMaster statusMaster = new StatusMaster
            {
                StatusID = status.StatusID,
                StatusName = status.StatusName
            };
            string checkColumn = ValidationHelper.CheckCompulsoryStatusColumn(statusMaster);
            if (checkColumn != QueryResource.AllFieldsPresent)
            {
                throw new Exception(checkColumn);
            }

            dc.StatusMasters.InsertOnSubmit(statusMaster);
            dc.SubmitChanges();
        }
        public void AddEmployeeProjectMap(EmployeeProject employeeProject)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            EmployeeProject employeeProject1 = new EmployeeProject
            {
                EmployeeProjectMapID = employeeProject.EmployeeProjectMapID,
                EmployeeID = employeeProject.EmployeeID,
                ProjectID = employeeProject.ProjectID,
                RoleID = employeeProject.RoleID
            };
            dc.EmployeeProjects.InsertOnSubmit(employeeProject1);
            dc.SubmitChanges();
        }
        public void AddEmployeeTaskMap(EmployeeTaskMap employeeTask)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            EmployeeTaskMap employeeTask1 = new EmployeeTaskMap
            {
                EmployeeTaskMapID = employeeTask.EmployeeTaskMapID,
                EmployeeID = employeeTask.EmployeeID,
                TaskID = employeeTask.TaskID
            };
            dc.EmployeeTaskMaps.InsertOnSubmit(employeeTask1);
            dc.SubmitChanges();
        }
        public void AddProjectTaskMap(ProjectTaskMap projectTask)

        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            ProjectTaskMap projectTask1 = new ProjectTaskMap
            {
                ProjectTaskMapID = projectTask.ProjectTaskMapID,
                ProjectID = projectTask.ProjectID,
                TaskID = projectTask.TaskID
            };
            dc.ProjectTaskMaps.InsertOnSubmit(projectTask1);
            dc.SubmitChanges();

        }
        public void AddTechProjectMap(TechProjectMap techProject)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            TechProjectMap techProject1 = new TechProjectMap
            {
                TechProjectMapID = techProject.TechProjectMapID,
                TechID = techProject.TechID,
                ProjectID = techProject.ProjectID
            };
            dc.TechProjectMaps.InsertOnSubmit(techProject1);
            dc.SubmitChanges();

        }
        public void AddTechTaskMap(TechTaskMap techTask)
        {
            CompanyDBDataContext dc = new CompanyDBDataContext();
            TechTaskMap techTask1 = new TechTaskMap
            {
                TechTaskMapID = techTask.TechTaskMapID,
                TechID = techTask.TechID,
                TaskID = techTask.TaskID
            };
            dc.TechTaskMaps.InsertOnSubmit(techTask1);
            dc.SubmitChanges();
        }
        
        public int  GetAllProjectOfTechnology(int technologyID)
        {
            try
            {
                if (ValidationHelper.IfTechnologyExist(technologyID))
                {
                    CompanyDBDataContext dc = new CompanyDBDataContext();
                    var projectList = (from techProject in dc.TechProjectMaps
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

                    var techProject = (from peoject in dc.TechProjectMaps where peoject.ProjectID == projectID select peoject.TechnologyMaster).ToList();
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
            var techTask = (from task in dc.TechTaskMaps where task.TaskID == taskID select task.TechID).ToList();
            return techTask.Count;
        }
        public int GetStatusOfTask(int taskID)
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
