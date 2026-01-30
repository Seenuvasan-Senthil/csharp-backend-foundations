using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Entities
{
    public class PermanentEmployee : Employee
    {
        public PermanentEmployee(string name, string department, decimal salary) : base(name, department, salary)
        {
        }
    }
}
