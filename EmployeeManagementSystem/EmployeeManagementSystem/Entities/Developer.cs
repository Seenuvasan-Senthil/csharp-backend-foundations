using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Entities
{
    public class Developer : Employee
    {
        public string Area { get; }

        public Developer(string name, string department, decimal salary, string area)
            : base(name, department, salary)
        {
            Area = area;
        }

        public override string GetEmployeeDetails()
        {
            return $"{base.GetEmployeeDetails()}, Area: {Area}";
        }
    }
}
