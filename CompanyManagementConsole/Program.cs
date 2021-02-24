using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementBusinessLayer;
using Newtonsoft.Json;

namespace CompanyManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           BusinessManager businessManager= new BusinessManager();
            /* DataManager dataManager = new DataManager();
           Console.WriteLine("***************************************GET ALL PROJECTS********************");
           foreach (var project in dataManager.GetAllProjects()) 
           {

               Console.WriteLine(project.ProjectName);

           }
           Console.WriteLine("***************************************GET ALL TECHNOLOGIES********************");
           foreach (TechnologyMaster technology in dataManager.GetAllTechologies())
           {
               Console.WriteLine(technology.TechName);

           }
           Console.WriteLine("***************************************GET EMPLOYEES FOR PROJECT********************");

           foreach (var project in dataManager.GetEmployeesForProject(1))
           {

               Console.WriteLine(project.EmployeeName);

           }
           Console.WriteLine(dataManager.GetEmployeeCountForProject(1));
           Console.WriteLine("***************************************GET ALL  DELAYED PROJECT********************");
           foreach(var project in dataManager.GetAllDelayedProjects())
           {
               Console.WriteLine(project.ProjectName);
           }

           Console.WriteLine("***************************************GET ALL PROJECT FOR EMPLOYEES********************");
           foreach (var employee in dataManager.GetAllProjectsForEmployee(1))
           {
               Console.WriteLine(employee.ProjectName);
           }
           Console.WriteLine("***************************************Get All Tasks For Employee********************");
           foreach (var task in dataManager.GetAllTasksForEmployee(1))
           {
               Console.WriteLine(task.TaskName);
           }
           Console.WriteLine("***************************************Get All Technology Tasks For Employee********************");
           foreach (var task in dataManager.GetAllTechnologyTasksForEmployee(1,1))
           {

               Console.WriteLine(task.Task.TaskName);
           }
           Console.WriteLine("***************************************GetAllTechnologyProjects********************");
           foreach (var techProjects in dataManager.GetAllTechnologyProjects(1))
           {

               Console.WriteLine(techProjects.ProjectName);
           }
           Console.WriteLine("***************************************GetAllActiveTasksForProject********************");

           foreach (var task in dataManager.GetAllActiveTasksForProject(1))
           {

               Console.WriteLine(task.TaskName);
           }
           Console.WriteLine("***************************************GetAllTechnologiesForEmployee********************");
           foreach (var tech in dataManager.GetAllTechnologiesForEmployee(1))
           {

               Console.WriteLine(tech.TechName);
           }
           Console.WriteLine("***************************************GetProjectCountForEmployee********************");
           Console.WriteLine(dataManager.GetProjectCountForEmployee(1));
           Console.WriteLine("***************************************GetAllActiveProjectsManagedByEmployee********************");

           foreach (var project in dataManager.GetAllActiveProjectsManagedByEmployee(1))
           {

               Console.WriteLine(project.ProjectName);
           }
           Console.WriteLine("***************************************DeleteEmployeeFromSystem********************");
          //dm.DeleteEmployeeFromSystem(7);
           Console.WriteLine("***************************************AddEmployee********************");
           Project addproject  = new Project();

           addproject.ProjectName = "Android App";
           addproject.ProjectBudget = 2000;
           addproject.ClientID = 2;
           addproject.StatusID = 2;
           // dm.AddProject(addproject);
           Console.WriteLine("***************************************GetAllDelayedTasksForEmployee********************");
           foreach(var empTask in dataManager.GetAllDelayedTasksForEmployee(2))
           {
               Console.WriteLine(empTask.TaskName);
           }
           //  dm.AssignEmployeeToProject(1, 4);
           //  dm.DeleteProject(1);
           //  dm.DeleteTask(1);
           //  dm.DeleteEmployee(1);
           //  Query is not affecting the data in database
           //  int[] techArray = { 1, 2, 3, 4, 5 };
           //  List<int> technologies = new List<int>(techArray);
           //  dm.UpdateTechnologiesForTask(technologies, 3);
           //  dm.AssignTechnologyToTask(3, 2);
           //  dm.UpdateStatusOfProject(4, 3);
           //  dm.CreateTaskInProject()

           // businessManager.DeleteTechnologyOFProject(4);
          *//* TechTaskMap techTask = new TechTaskMap();
           techTask.TechID = 2;
           techTask.TaskID = 2;
           businessManager.BMAddTechTask(techTask);*//*
          // businessManager.DeleteTask(9);


           CompanyManagementDatalayer.Task ts = new CompanyManagementDatalayer.Task();
           ts.TaskName = "CloudLAyer";
           ts.StatusID = 2;
           dataManager.AddTask(ts);
           businessManager.BMCreateTaskInProject(ts, 1);*/
           
        
            var project = businessManager.GetAllProjects();
            var obj = JsonConvert.SerializeObject(project);
            Console.WriteLine(obj);
            Console.ReadKey();
        }
    }
}
