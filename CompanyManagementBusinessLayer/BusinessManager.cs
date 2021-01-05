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
        public void BMDeleteTechnology(int technologyID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int techCount = dataManger.GetAllProjectOfTechnology(technologyID);
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
        public void BMAddTechTask(TechTaskMap techTask)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int count = dataManger.GetAllTechnologyForTask(techTask.TaskID);
                if (count < Convert.ToInt32(QueryResource.MaxTechAssignedToProject))
                {
                    if (!ValidationHelper.IsTechPresentInTask(techTask.TechID, techTask.TaskID))
                    {
                        dataManger.AddTechTaskMap(techTask);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechPresentInTask);
                    }
                }
                else
                {
                    throw new Exception(QueryResource.TechnologyTaskExceeds);
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
                int validateStatus = dataManger.GetStatusOfTask(TaskID);
                if (validateStatus != (int)StatusEnum.Active)
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
                int checkStatus = dataManger.GetStatusOfProject(projectID);
                if (checkStatus != (int)StatusEnum.Active)
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
                        throw new Exception(QueryResource.ProjectIDNotFound);
                    }
                    else if (projectPresent)
                    {
                        throw new Exception(QueryResource.TaskNotExist);
                    }
                    else
                    {
                        throw new Exception(QueryResource.TechAndTaskNotExist);
                    }
                }
            }
            catch (Exception ex) { throw ex; }


        }
       // public List<Project> GetAllProjects()
        {

        }




    }
}
