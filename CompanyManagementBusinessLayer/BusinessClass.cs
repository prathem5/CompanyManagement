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
                DataManger dataManger = new DataManger();
                int techCount = dataManger.getAllProjectOfTechnology(technologyID);
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
                DataManger dataManger = new DataManger();
                int count = dataManger.getAllTechnologyForTask(taskID);
                if (count < 4)
                {
                    TechTaskMap tt = new TechTaskMap();
                    dataManger.AddTechTaskMap(techTask, mapID, techID, taskID);
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
                DataManger dataManger = new DataManger();
                int checkStatus = dataManger.getStatusOfTask(TaskID);
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
                DataManger dataManger = new DataManger();
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
                DataManger dataManger = new DataManger();
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
                DataManger dataManger = new DataManger();
                if (dataManger.isManager(employeeID) && dataManger.GetProjectCountForEmployee(employeeID) < 3)
                {
                    dataManger.AddEmployeeProjectMap(project, mapID, employeeID, projectID, roleID);
                }
                else if (dataManger.isWorker(employeeID) && dataManger.GetProjectCountForEmployee(employeeID) < 2)
                {
                    dataManger.AddEmployeeProjectMap(project, mapID, employeeID, projectID, roleID);

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
