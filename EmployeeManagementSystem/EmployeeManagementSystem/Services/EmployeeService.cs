using System;
using System.Text;
using EmployeeManagementSystem.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;
        private int _nextId;

        public EmployeeService()
        {
            _employees = new List<Employee>();
            _nextId = 1;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            employee.SetId(_nextId);
            _nextId++;

            _employees.Add(employee);
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(_employees);
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
