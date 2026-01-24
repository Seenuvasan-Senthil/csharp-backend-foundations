using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Entities
{
    public class Manager : Employee
    {
        //manager's specific fields are here
        private int _teamSize; //uncontrolled access prevention
        
        //controlled access via property below
        public int TeamSize
        {
            get { return _teamSize; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("TeamSize Cannot be Empty .");
                _teamSize = value;
            }
        }

        //paramettarized constructor - creates valid object
        public Manager(int id, string name, string department, decimal salary, int teamSize) : base(id, name, department, salary)
        {
            TeamSize = teamSize;
        }

        //polymorphism - manager specific details - overriding generic emp method
        public override string GetEmployeeDetails()
        {
            return $"{base.GetEmployeeDetails()}, TeamSize: {TeamSize}"; //"base"->like "super" keyword in java
        }
    }
}
