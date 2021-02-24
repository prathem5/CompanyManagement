using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagementDatalayer;

namespace CompanyManagementBusinessLayer.Entities
{
     public class BOEmployee
    {
        public int EmployeeID;
        public string EmployeeName;
        public string EmployeeAddress;
        public int EmployeeSalary;
        public DepartmentMaster Department;

    }
}
