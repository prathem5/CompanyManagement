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
            //***************************************************GET ALL PROJECTS*************************************************
            /*  foreach (var project in dm.GetAllProjects())
              {

                  Console.WriteLine(project.ProjectName);

              }*/
            //****************************************************GET ALL TECHNOLOGIES************************************************
            /* foreach (TechnologyMaster technology in dm.GetAllTechologies())
             {
                 Console.WriteLine(technology.TechName);

             }*/
            //*****************************************************GET EMPLOYEES FOR PROJECT**********************************************

            foreach (var project in dm.GetEmployeesForProject(1))
            {

                Console.WriteLine(project.EmployeeName);
                dm.AssignTechnologyToTask()
            }

                Console.ReadKey();
        }
    }
}
