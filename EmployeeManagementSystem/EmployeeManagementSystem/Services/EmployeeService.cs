using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            //employee.SetId(_nextId);
            _nextId++;

            _employees.Add(employee);
            Console.WriteLine("entering db: ");
            using var connection = DatabaseHelper.GetConnection();
            try
            {
                connection.Open();
                string query = @"
                    INSERT INTO EMPLOYEES(Name, Department, Salary, ManagerId)
                    VALUES(@Name, @Department, @Salary, @ManagerId);
                    SELECT SCOPE_IDENTITY();
                    ";

                using var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@ManagerId", (object?)employee.ManagerId ?? DBNull.Value);
            }
            catch
            {
                Console.WriteLine("error in connecting.");
            }

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

            employee.UpdateSalary(newSalary); //runtime polymorphism
        }

        public void RemoveEmployee(int id)
        {
            var employee = GetEmployeeById(id);

            if (employee == null) throw new InvalidOperationException("Employee not Found");

            _employees.Remove(employee);
        }

        public int GetTeamSize(int managerId)  //team size is now calculated using emp service dynamic
        {
            return _employees.Count(e => e.ManagerId == managerId);
        }

    }
}
