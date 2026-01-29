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
            Employee emp = new PermanentEmployee("venkat", "strata", 50000);

            emp.AssignRole(new ManagerRole());
            emp.AssignRole(new DeveloperRole());

            if (emp.HasRole<IApprover>())
            {
                Console.WriteLine($"{emp.Name} can approve reqs");
                return;
            }

            IEmployeeService employeeService = new EmployeeService();
            RequestService requestService = new RequestService();
            //constructor dependency injection
            MainMenu mainMenu = new MainMenu(employeeService, requestService);
            mainMenu.show();

            Console.WriteLine("Application Closed.");
        }
    }
}