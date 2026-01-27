using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Enums;

namespace EmployeeManagementSystem.Menus
{
    internal class EmployeeMenu
    {

        private readonly IEmployeeService _employeeService;
        private readonly RequestService _requestService;
        private readonly Role _role;

        public EmployeeMenu(IEmployeeService employeeService, RequestService requestService, Role role)
        {
            _employeeService = employeeService;
            _requestService = requestService;
            _role = role;
        }

        public void Show()
        {
            Employee employee = AuthenticateEmployee();

            if(employee == null)
            {
                Console.WriteLine("Invalid emp Id or Role Mismatch");
                return;
            }

            bool logout = false;

            while (!logout)
            {
                Console.WriteLine($"\n---{_role} Menu---");
                Console.WriteLine("1. View Profile.");
                Console.WriteLine("2. Raise Request");
                Console.WriteLine("0. Logout");
                Console.WriteLine("Choose Option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowProfile(employee);
                        break;

                    case "2":
                        RaiseRequest(employee);
                        break;

                    case "0":
                        logout = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Operation.");
                        break;
                }
            }
        }

        private Employee AuthenticateEmployee()
        {
            Console.WriteLine("Enter Employee ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
                return null;

            Employee employee = _employeeService.GetEmployeeById(id);

            if (employee == null) return null;

            //role vadation part
            if (_role == Role.Manager && employee is not Manager) return null;

            if (_role == Role.Tester && employee is not Tester) return null;

            return employee;
        }

        private void ShowProfile(Employee employee)
        {
            Console.WriteLine("\n---My Profile---");
            Console.WriteLine($"ID: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Salary: {employee.Salary}");
        }

        private void RaiseRequest(Employee employee)
        {
            Console.WriteLine("Enter Request Message: ");
            string desc = Console.ReadLine();
            var request = new Request(employee.Id, _role, desc);
            _requestService.CreateRequest(request);
            Console.WriteLine("Request Raised Successfully.");
        }
    }
}
