using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Entities
{
    //abstract->controls whether(instantiated or not)
    //this constructor protected->controls who
    public abstract class Employee{  //Employee is a base domain concepts that must be visible but never be instantiated directly.
        private int _id;      
        private string _name;
        private string _department;
        private decimal _salary;  //private->prevents uncontrolled modification

        //properties(controlled access)
        public int Id { get; private set; }

        public string Name { get; }

        public string Department { get; }

        public decimal Salary { get; private set; }

        public void UpdateSalary(decimal newSalary)
        {
            if (newSalary < 0) throw new ArgumentException("Salary Must Be Positive");
            Salary = newSalary;  //encapsulation
        }

        //parametarized constructor -> creating a valid employee -> to avoid using if, else everywhere.
        protected Employee(string name, string department, decimal salary) //protected->Because Employee is a base domain concept and should only be instantiated through concrete derived types.
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is Required");
            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Dept is Required");
            if (salary < 0)
                throw new ArgumentException("Salary cant negative");

            Name = name;
            Department = department;
            Salary = salary;
        }

        //common behaviour 
        public virtual string GetEmployeeDetails()  //virtual->Bcoz specific roles like Developer, etc may override it.
        {
            return $"Id: {Id}, Name: {Name}, Department: {Department}, Salary: {Salary}";
        }
    }
}
