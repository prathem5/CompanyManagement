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
        public void DeleteTechnologyOFProject(int technologyID)
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

      /*  public void CreateTaskIntProject(int taskID, int projectID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int checkStatus = dataManger.GetStatusOfProject(projectID);
                if (checkStatus != (int)StatusEnum.Completed)
                {

                    dataManger.CreateTaskInProject(taskID, projectID);
                }
                else
                {
                    throw new Exception(QueryResource.ProjectCompleted);
                }
            }
            catch (Exception ex) { throw ex; }
        }*/
        public void AssignProjectToEmployee(EmployeeProject project)
        {
            try
            {
                DataManager dataManger = new DataManager();
                if (ValidationHelper. IsManager(employeeID) && dataManger.GetProjectCountForEmployee(employeeID) < 3)
                {
                    dataManger.AddEmployeeProjectMap(project);
                }
                else if (ValidationHelper.IsWorker(employeeID) && dataManger.GetProjectCountForEmployee(employeeID) < 2)
                {
                    dataManger.AddEmployeeProjectMap(project);

                }
                else
                {
                    throw new Exception(QueryResource.EmployeeNotInContext);
                }
            }catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
