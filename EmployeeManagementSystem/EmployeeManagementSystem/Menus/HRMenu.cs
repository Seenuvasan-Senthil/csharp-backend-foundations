using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Services;
using System;
using System.Net.Security;

namespace EmployeeManagementSystem.Menus
{
    internal class HRMenu
    {
        private const int HR_SECRET_KEY = 10;
        private readonly IEmployeeService _employeeService;

        public HRMenu(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Show()
        {
            if (!Authenticate())
            {
                Console.WriteLine("Invalid Hr Key. So, Access deied.");
                return;
            }

            bool logout = false;

            while (!logout)
            {
                Console.WriteLine("\n---HR Menu---");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Remove Employee");
                Console.WriteLine("3. View All Employee");
                Console.WriteLine("4. Update Salary");
                Console.WriteLine("0. Logout");
                Console.WriteLine("Choose Option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //Add
                        break;

                    case "2":
                        //Remove
                        break;

                    case "3":
                        //ViewAll
                        break;

                    case "4":
                        //UpdateSalary
                        break;

                    case "0":
                        logout = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Option .");
                        break;
                }
            }
        }

        private bool Authenticate()
        {
            Console.WriteLine("Enter Hr Secret Key: ");
            return int.TryParse(Console.ReadLine(), out int key) && key == HR_SECRET_KEY; 
        }
    }
}
