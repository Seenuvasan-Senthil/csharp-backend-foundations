using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Services;
using System;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmployeeService employeeService = new EmployeeService();

            //create a employee
            Employee emp1 = new Manager(
                name: "Suresh",
                department: "Strata",
                salary: 100000,
                teamSize: 5
                );

            //Add employee .
            employeeService.AddEmployee(emp1);

            //Display all Employee
            Console.WriteLine("Employees: ");

            foreach(var emp in employeeService.GetAllEmployees())
            {
                Console.WriteLine(
                    $"ID: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}"
                    );
            }

            Console.WriteLine("Updating Salary");

            //polymorphism-manager's specific salary
            employeeService.UpdateSalary(1, 100000);

            foreach(var emp in employeeService.GetAllEmployees())
            {
                Console.WriteLine(
                    $"ID: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}, Salary: {emp.Salary}"
                    );
            }

            Console.WriteLine("\n Press any Key to Exit...");
            Console.ReadKey();
        }
    }
}