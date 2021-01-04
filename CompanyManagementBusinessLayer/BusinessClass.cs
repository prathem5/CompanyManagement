using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;
using CompanyManagementBusinessLayer.Entities;


namespace CompanyManagementBusinessLayer
{
    public class BusinessClass
    {
        public void DeleteTechnologyOFProject(int technologyID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int techCount = dataManger.GetAllProjectOfTechnology(technologyID);
                if (techCount < 2)
                {
                    dataManger.DeleteTechnology(technologyID);
                }
                else {
                    throw new Exception(QueryResource.TechnologyCannotBeDeleted);
                }

            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddTechTask(TechTaskMap techTask, int mapID, int techID, int taskID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int count = dataManger.GetAllTechnologyForTask(taskID);
                if (count < 4)
                {
                    TechTaskMap tt = new TechTaskMap();
                    dataManger.AddTechTaskMap(techTask);
                }
                else
                {
                    throw new Exception(QueryResource.TechnologyTaskExceeds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void DeleteTask(int TaskID)
        {
            try
            {
                DataManager dataManger = new DataManager();
                int checkStatus = dataManger.GetStatusOfTask(TaskID);
                if (checkStatus != (int)StatusEnum.Active)
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

        public void CreateTaskIntProject(int taskID, int projectID)
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
        }
        public void AssignProjectToEmployee(EmployeeProject project, int mapID,int employeeID, int projectID, int roleID)
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
