using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;

namespace CompanyManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager dm = new DataManager();
            Console.WriteLine("***************************************GET ALL PROJECTS********************");
            foreach (var project in dm.GetAllProjects()) 
            {

                Console.WriteLine(project.ProjectName);

            }
            Console.WriteLine("***************************************GET ALL TECHNOLOGIES********************");
            foreach (TechnologyMaster technology in dm.GetAllTechologies())
            {
                Console.WriteLine(technology.TechName);

            }
            Console.WriteLine("***************************************GET EMPLOYEES FOR PROJECT********************");

            foreach (var project in dm.GetEmployeesForProject(1))
            {

                Console.WriteLine(project.EmployeeName);

            }
            Console.WriteLine(dm.GetEmployeeCountForProject(1));
            Console.WriteLine("***************************************GET ALL  DELAYED PROJECT********************");
            foreach(var project in dm.GetAllDelayedProjects())
            {
                Console.WriteLine(project.ProjectName);
            }

            Console.WriteLine("***************************************GET ALL PROJECT FOR EMPLOYEES********************");
            foreach (var employee in dm.GetAllProjectsForEmployee(1))
            {
                Console.WriteLine(employee.ProjectName);
            }
            Console.WriteLine("***************************************Get All Tasks For Employee********************");
            foreach (var task in dm.GetAllTasksForEmployee(1))
            {
                Console.WriteLine(task.TaskName);
            }
            Console.WriteLine("***************************************Get All Technology Tasks For Employee********************");
            foreach (var task in dm.GetAllTechnologyTasksForEmployee(1,1))
            {
                
                Console.WriteLine(task.Task.TaskName);
            }
            Console.WriteLine("***************************************GetAllTechnologyProjects********************");
            foreach (var techProjects in dm.GetAllTechnologyProjects(1))
            {

                Console.WriteLine(techProjects.ProjectName);
            }
            Console.WriteLine("***************************************GetAllActiveTasksForProject********************");

            foreach (var task in dm.GetAllActiveTasksForProject(1))
            {

                Console.WriteLine(task.TaskName);
            }
            Console.WriteLine("***************************************GetAllTechnologiesForEmployee********************");
            foreach (var tech in dm.GetAllTechnologiesForEmployee(1))
            {

                Console.WriteLine(tech.TechName);
            }
            Console.WriteLine("***************************************GetProjectCountForEmployee********************");
            Console.WriteLine(dm.GetProjectCountForEmployee(1));
            Console.WriteLine("***************************************GetAllActiveProjectsManagedByEmployee********************");
            dm.AssignStatusToProject(1, 2);
            foreach (var project in dm.GetAllActiveProjectsManagedByEmployee(1))
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
            foreach(var empTask in dm.GetAllDelayedTasksForEmployee(2))
            {
                Console.WriteLine(empTask.TaskName);
            }
            //  dm.AssignEmployeeToProject(1, 4);
            //  dm.DeleteProject(1);
            //   dm.DeleteTask(1);
            List<int> techID = ;
             dm.UpdateTechnologiesForTask()
        }
    }
}
