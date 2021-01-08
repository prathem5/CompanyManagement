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
                if (techCount < Convert.ToInt32(QueryResource.TechnologyUsedInMaximumProjects))
                {
                    dataManger.DeleteTechnology(technologyID);
                }
                else
                {
                    throw new Exception(QueryResource.TechnologyCannotBeDeleted);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public void AssignTEchnologyToTAsk(int taskID, int technologyID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                var projectList = dataManager.GEtProjectListOfTask(taskID);
                foreach (var project in projectList)
                {
                    if (ValidationHelper.DoesProjectHasTech(project.ProjectID, technologyID))
                    {
                        dataManager.AssignTechnologyToTask(technologyID, taskID);
                    }

                }
            }
            catch (Exception ex) { throw ex; }

        }
       
        public void DeleteTask(int TaskID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int taskStatus = dataManger.GetStatusOfTask(TaskID);
                if (taskStatus == (int)StatusEnum.NotStarted)
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
                if (projectStatus == (int)StatusEnum.NotStarted)
                {
                    dataManger.DeleteProject(projectID);

                }
                else
                {
                    throw new Exception(QueryResource.ProjectActive);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateTaskInProject(CompanyManagementDatalayer.Task task, int projectID)
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

                    if (currentStatus != projectCompleted)
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
        public void AddTechnologyToTask(TechTaskMap techTask)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int count = dataManger.GetTechnologyCountForTask(techTask.TaskID);
                if (count < Convert.ToInt32(QueryResource.TechnologyAssignedToMaximumProject))
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
        public List<BOProject> GetAllProjects()
        {
            try
            {
                DataManager dataManager = new DataManager();
                DataConverter converter = new DataConverter();
                List<BOProject> boProjectList = converter.ConvertToBOProjectList(dataManager.GetAllProjects());
                return boProjectList;
            }
            catch (Exception ex) { throw ex; }


        }
        public List<BOTechnologyMaster> GetAllTechnologies()
        {

            try
            {
                DataManager dataManager = new DataManager();
                DataConverter converter = new DataConverter();
                List<BOTechnologyMaster> boTechnologyList = converter.ConvertToBOTechnologyMasterList(dataManager.GetAllTechologies());
                return boTechnologyList;
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
        public List<BOEmployee> GetAllEmployeesForProject(int projectID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                DataConverter converter = new DataConverter();
                List<BOEmployee> employeeList = converter.ConvertToBOEmployeeList(dataManager.GetEmployeesForProject(projectID));
                return employeeList;
            }
            catch (Exception ex) { throw ex; }

        }
        public List<BOProject> GetAllDelayedProjects()
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOProject> projectList = converter.ConvertToBOProjectList(dataManager.GetAllDelayedProjects());
                return projectList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BOProject> GetAllProjectsForEmployee(int employeeID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOProject> projectList = converter.ConvertToBOProjectList(dataManager.GetAllProjectsForEmployee(employeeID));
                return projectList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BOTask> GetAllTasksForEmployee(int employeeID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOTask> taskList = converter.ConvertToBOTaskList(dataManager.GetAllTasksForEmployee(employeeID));
                return taskList;
            }
            catch (Exception ex) { throw ex; }

        }
        public List<BOTechTaskMap> GetAllTechnologyTasksForEmployee(int technologyID, int employeeID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOTechTaskMap> techList = converter.ConvertToBOTechTaskList(dataManager.GetAllTechnologyTasksForEmployee(technologyID, employeeID));

                return techList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BOProject> GetAllTechnologyProjects(int technologyID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOProject> techProjectList = converter.ConvertToBOProjectList(dataManager.GetAllTechnologyProjects(technologyID));
                return techProjectList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BOTask> GetAllActiveTasksForProject(int projectID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOTask> taskList = converter.ConvertToBOTaskList(dataManager.GetAllActiveTasksForProject(projectID));
                return taskList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BOTechnologyMaster> GetAllTechnologiesForEmployee(int employeeID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOTechnologyMaster> techList = converter.ConvertToBOTechnologyMasterList(dataManager.GetAllTechnologiesForEmployee(employeeID));
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
        public List<BOProject> GetAllActiveProjectsManagedByEmployee(int employeeID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOProject> projectManagerList = converter.ConvertToBOProjectList(dataManager.GetAllActiveProjectsManagedByEmployee(employeeID));
                return projectManagerList;
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BOTask> GetAllDelayedTasksForEmployee(int employeeID)
        {
            try
            {
                DataConverter converter = new DataConverter();
                DataManager dataManager = new DataManager();
                List<BOTask> taskList = converter.ConvertToBOTaskList(dataManager.GetAllDelayedTasksForEmployee(employeeID));
                return taskList;
            }
            catch (Exception ex) { throw ex; }

        }
        public void AddProject(Project project)
        {
            try
            {
                DataManager dataManager = new DataManager();
                dataManager.AddProject(project);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddTechnology(TechnologyMaster technology)
        {
            try
            {
                DataManager dataManager = new DataManager();
                dataManager.AddTechnology(technology);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddEmployee(Employee employee)
        {
            try
            {
                DataManager dataManager = new DataManager();
                dataManager.AddEmployee(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public void AssignEmployeeToProject(int employeeID, int projectID, )
        {
            try
            {
             
                    DataManager dataManager = new DataManager();
                    
                    int projectCountForEmployee = dataManager.GetProjectCountForEmployee(employeeID);
                    int projectManagedCountForEmployee = dataManager.GetAllActiveProjectsManagedByEmployee(employeeID).Count();
                if (ValidationHelper.IsManager(employeeID))
                {
                    if (projectManagedCountForEmployee >= Convert.ToInt32(QueryResource.MaximumNumberOfProjectForManager))
                    {
                        throw new Exception(QueryResource.CannotAssignMoreProjectForManager);
                    }
                    else if (projectManagedCountForEmployee < Convert.ToInt32(QueryResource.MaximumNumberOfProjectForManager))
                    {
                        dataManager.AssignEmployeeToProject(employeeID, projectID);
                    }


                }else if (ValidationHelper.IsWorker(employeeID))
                {
                    if (projectCountForEmployee >= Convert.ToInt32(QueryResource.CannotAssignMoreProjectsForEmployee))
                    {
                        throw new Exception(QueryResource.CannotAssignMoreProjectsForEmployee);
                    }else if (projectManagedCountForEmployee < Convert.ToInt32(QueryResource.CannotAssignMoreProjectsForEmployee))
                    {
                        dataManager.AssignEmployeeToProject(employeeID, projectID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        public void UpdateTechnologiesForTask(List<int> technologyIDs, int taskID)
        {
            try
            {
                DataManager dataManager = new DataManager();
                dataManager.UpdateTechnologiesForTask(technologyIDs, taskID);

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
                DataManager dataManager = new DataManager();
                dataManager.DeleteEmployeeFromSystem(employeeID);

            }

            catch (Exception ex)
            {
                throw ex;

            }
        }

    }






}

