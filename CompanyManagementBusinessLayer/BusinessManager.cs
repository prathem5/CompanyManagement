using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;
using CompanyManagementBusinessLayer.Entities;


namespace CompanyManagementBusinessLayer
{
    public class BusinessManager
    {
        public void DeleteTechnology(int technologyID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int techCount = dataManger.GetProjectCountOfTechnology(technologyID);
                if (techCount < Convert.ToInt32(QueryResource.MaxProjectUsingTech))
                {
                    dataManger.DeleteTechnology(technologyID);
                }
                else {
                    throw new Exception(QueryResource.TechnologyCannotBeDeleted);
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public void AssignTEchnologyToTAsk(int taskID,int technologyID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                var projectList = dataManager.GEtProjectListOfTask(taskID);
                foreach (var project in projectList)
                {
                    if (ValidationHelper.DoesProjectHasTech(project.ProjectID, technologyID)
                    {
                        dataManager.AssignTechnologyToTask(technologyID, taskID);
                    }

                }
            }catch (Exception ex) { throw ex; }

        }
        public void AddTechnologyToTask(TechTaskMap techTask)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int count = dataManger.GetTechnologyCountForTask(techTask.TaskID);
                if (count < Convert.ToInt32(QueryResource.MaxTechAssignedToProject))
                {
                    if (!ValidationHelper.IsTechPresentInTask(techTask.TechID, techTask.TaskID))
                    {
                        dataManger.AddTechTaskMap(techTask);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechnologyPresentInTask);
                    }
                }
                else
                {
                    throw new Exception(QueryResource.TechnologiesOfTaskExceeds);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
               
            }

        }
        public void DeleteTask(int TaskID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int taskStatus = dataManger.GetStatusOfTask(TaskID);
                if (taskStatus != (int)StatusEnum.Active && taskStatus != (int)StatusEnum.Delayed)
                {
                    dataManger.DeleteTask(TaskID);
                }
                else
                {
                    throw new Exception(QueryResource.TaskActiveState);

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
                DataManager dataManger = new DataManager();
                int projectStatus = dataManger.GetStatusOfProject(projectID);
                if (projectStatus != (int)StatusEnum.Active && projectStatus != (int)StatusEnum.Delayed)
                {
                    dataManger.DeleteProject(projectID);

                }
                else
                {
                    throw new Exception(QueryResource.ProjectActive);
                }

            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BMCreateTaskInProject(CompanyManagementDatalayer.Task task, int projectID)
        {
            try
            {
                bool taskPresent = ValidationHelper.IfTaskExist(task.TaskID);
                bool projectPresent = ValidationHelper.IfProjectExist(projectID);
                if (projectPresent && taskPresent)
                {
                    DataManager dataManager = new DataManager();
                    int currentStatus = dataManager.GetStatusOfProject(projectID);
                    int projectCompleted = (int)StatusEnum.Completed;
                   
                    if(currentStatus != projectCompleted)
                    {

                        dataManager.CreateTaskInProject(task, projectID);
                    }
                    else
                    {
                        throw new Exception(QueryResource.ProjectCompleted);
                    }

                }
                else if (!taskPresent || !projectPresent)
                {
                    if (taskPresent)
                    {
                        throw new Exception(QueryResource.ProjectNotFound);
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
        public List<Project> GetAllProjects()
        {
            try
            {
                    DataManager dataManager = new DataManager();
                    List<Project> projectList = dataManager.GetAllProjects();
                    return projectList;
            }
            catch (Exception ex) { throw ex; }
           
            
        }
        public List<TechnologyMaster> GetAllTechnologies()
        {
            try
            { 

                    DataManager dataManager = new DataManager();
                    List<TechnologyMaster> technologyList = dataManager.GetAllTechologies();
                    return technologyList;
            }
            catch (Exception ex) { throw ex; }
            

        }
        public int GetEmployeeCountForProject(int projectID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                int employeeCount = dataManager.GetEmployeeCountForProject(projectID);
                return employeeCount;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<Employee> GetAllEmployeesForProject(int projectID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<Employee> employeeList = dataManager.GetEmployeesForProject(projectID);
                return employeeList;
            }
            catch (Exception ex) { throw ex; }

        }
         public List<Project> GetAllDelayedProjects() 
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<Project> projectList = dataManager.GetAllDelayedProjects();
                return projectList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<Project> GetAllProjectsForEmployee(int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<Project> projectList = dataManager.GetAllProjectsForEmployee(employeeID);
                return projectList;
            }
            catch (Exception ex) { throw ex; }
        }
       public List<CompanyManagementDatalayer.Task> GetAllTasksForEmployee(int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<CompanyManagementDatalayer.Task> taskList = dataManager.GetAllTasksForEmployee(employeeID);
                return taskList;
            }
            catch (Exception ex) { throw ex; }

        }
        public List<TechTaskMap> GetAllTechnologyTasksForEmployee(int technologyID, int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<TechTaskMap> techList = dataManager.GetAllTechnologyTasksForEmployee(technologyID, employeeID);
                return techList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<Project> GetAllTechnologyProjects(int technologyID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<Project> techProjectList = dataManager.GetAllTechnologyProjects(technologyID);
                return techProjectList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<CompanyManagementDatalayer.Task> GetAllActiveTasksForProject(int projectID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<CompanyManagementDatalayer.Task> taskList = dataManager.GetAllActiveTasksForProject(projectID);
                return taskList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<TechnologyMaster> GetAllTechnologiesForEmployee(int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<TechnologyMaster> techList = dataManager.GetAllTechnologiesForEmployee(employeeID);
                return techList;
            }
            catch (Exception ex) { throw ex; }
        }
        public int GetProjectCountForEmployee(int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                int projectCount = dataManager.GetProjectCountForEmployee(employeeID);
                return projectCount;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<Project> GetAllActiveProjectsManagedByEmployee(int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<Project> projectManagerList = dataManager.GetAllActiveProjectsManagedByEmployee(employeeID);
                return projectManagerList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<CompanyManagementDatalayer.Task> GetAllDelayedTasksForEmployee(int employeeID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                List<CompanyManagementDatalayer.Task> taskList = dataManager.GetAllDelayedTasksForEmployee(employeeID);
                return taskList;
            }
            catch (Exception ex) { throw ex; }

        }





    }
}
