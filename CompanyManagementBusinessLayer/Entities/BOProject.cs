using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;
namespace CompanyManagementBusinessLayer.Entities
{
  public  class BOProject
    {
        public int ProjectID;
        public string ProjectName;
        public int ProjectBudget;
        public StatusMaster statusID;
        public Client clientID;
    }
}
