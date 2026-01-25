using System;
using System.Text;
using EmployeeManagementSystem.Entities;
using System.Collections.Generic;
using System.Linq;

//EmployeeService.cs->Implementation
namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        //where do we store employees, private-no one outside should touch storage directly, readonly-reference should not change.
        private readonly List<Employee> _employees;  
        private int _nextId;

        public EmployeeService()  //constructor public-Program.cs needs to create the service, service is an entry point.
        {
            //gurantees valid state, no null risks
            _employees = new List<Employee>();
            _nextId = 1;
        }

        public void AddEmployee(Employee employee)  //we can pass developer, sre., etc - polymorphism used here
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            employee.SetId(_nextId);
            _nextId++;

            _employees.Add(employee);
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id); //no exception, caller decides how to handle null
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(_employees); //new list - prevents external modification, protects internal state - advanced encapsulation.
        }

        public void UpdateSalary(int id, decimal newSalary)
        {
            var employee = GetEmployeeById(id);

            if (employee == null) throw new InvalidOperationException("Employee not Found.");

            employee.UpdateSalary(newSalary);
        }

        public void RemoveEmployee(int id)
        {
            var employee = GetEmployeeById(id);

            if (employee == null) throw new InvalidOperationException("Employee not Found");

            _employees.Remove(employee);
        }

    }
}
