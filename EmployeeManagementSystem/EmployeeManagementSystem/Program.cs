using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Menus;
using System;
using EmployeeManagementSystem.Roles;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmployeeService employeeService = new EmployeeService();
            RequestService requestService = new RequestService();
         
            //constructor dependency injection.
            MainMenu mainMenu = new MainMenu(employeeService, requestService);
            mainMenu.show();

            Console.WriteLine("Application Closed.");
        }
    }
}