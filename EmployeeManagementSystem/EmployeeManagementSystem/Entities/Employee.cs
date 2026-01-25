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
        public int Id
        {
            get { return _id; }
            protected internal set
            {
                if (Id != 0) throw new InvalidOperationException("Id is Already Set");
                Id = value;
            }
        }

        public string Name
        {
            get { return _name; }
            //protected set {
            //    if (string.IsNullOrWhiteSpace(value))
            //        throw new ArgumentException("Name Cannot be Empty.");
            //    _name = value; }
        }

        public string Department
        {
            get { return _department; }
            //protected set
            //{
            //    if (string.IsNullOrWhiteSpace(value))
            //        throw new ArgumentException("Department cannot be Empty.");
            //    _department = value;
            //}
        }

        public decimal Salary
        {
            get { return _salary; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Salary Cannot be Negative");
                _salary = value;
            }
        }

        //parametarized constructor -> creating a valid employee -> to avoid using if, else everywhere.
        protected Employee(int id, string name, string department, decimal salary) //protected->Because Employee is a base domain concept and should only be instantiated through concrete derived types.
        {
            Id = id;
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
