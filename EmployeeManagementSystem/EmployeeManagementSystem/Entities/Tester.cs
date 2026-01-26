using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Entities
{
    public class Tester : Employee
    {
        public string TechStack { get; }

        public Tester(string name, string department, decimal salary, string techStack)
            :base(name, department, salary)
        {
            TechStack = techStack;
        }

        public override string GetEmployeeDetails()
        {
            return $"{base.GetEmployeeDetails()}, TechStack: {TechStack}";
        }
    }
}
