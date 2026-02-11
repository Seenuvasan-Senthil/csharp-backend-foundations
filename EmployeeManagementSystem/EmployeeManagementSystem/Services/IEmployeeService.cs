using EmployeeManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Services
{
    //IEmployeeService.cs->Contract only
    public interface IEmployeeService //interface-any implementations must provide these behaviours-abstraction in action
    {
        void AddEmployee(Employee employee); //our empservice should be able to do these behaviours.
        Employee GetEmployeeById(int id);
        List<Employee> GetAllEmployees();
        void UpdateSalary(int id, decimal newSalary);
        void RemoveEmployee(int id);

        int GetTeamSize(int managerId);

        public void AddCapabilityToEmployee(int employeeId, int capabilityId);
    }
}
