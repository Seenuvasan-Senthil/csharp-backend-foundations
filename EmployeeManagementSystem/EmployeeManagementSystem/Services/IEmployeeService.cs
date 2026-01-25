using EmployeeManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        Employee GetEmployeeById(int id);
        List<Employee> GetAllEmployees();
        void UpdateSalary(int id, decimal newSalary);
        void RemoveEmployee(int id);
    }
}
