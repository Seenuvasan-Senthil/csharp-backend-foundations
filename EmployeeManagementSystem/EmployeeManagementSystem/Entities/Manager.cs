using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Entities
{
    public class Manager : Employee
    {
        //manager's specific fields are here
        public int TeamSize { get; }

        //paramettarized constructor - creates valid object
        public Manager(string name, string department, decimal salary, int teamSize) : base(name, department, salary)
        {
            if (teamSize <= 0) throw new ArgumentException("TeamSize cannot be Empty.");
            TeamSize = teamSize;
        }

        //polymorphism-manager may have 10% bonus on emp salary, so polymorphism applied here. 
        public override void UpdateSalary(decimal newSalary)
        {
            decimal managerSalary = newSalary + (newSalary * 1.10m);
            base.UpdateSalary(managerSalary);
        }

        //polymorphism - manager specific details - overriding generic emp method
        public override string GetEmployeeDetails()
        {
            return $"{base.GetEmployeeDetails()}, TeamSize: {TeamSize}"; //"base"->like "super" keyword in java
        }
    }
}
