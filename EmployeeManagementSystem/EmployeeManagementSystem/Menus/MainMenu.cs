using EmployeeManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Enums;

namespace EmployeeManagementSystem.Menus
{
    internal class MainMenu
    {
        //we will need these two services to our application run
        private readonly IEmployeeService _employeeService;
        private readonly RequestService _requestService;

        //dependencies-MainMenu needs this services, receives it
        public MainMenu(IEmployeeService employeeService, RequestService requestService) //initialize services
        {
            _employeeService = employeeService;
            _requestService = requestService;
        }

        public void show() //journey starts
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n###Employee Management System###");
                Console.WriteLine("1. HR");
                Console.WriteLine("2. Manager");
                Console.WriteLine("3. Developer");
                Console.WriteLine("4. Tester");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Select your Role: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var hrMenu = new HRMenu(_employeeService, _requestService);
                        hrMenu.Show();
                        break;

                    case "2":
                        new EmployeeMenu(_employeeService, _requestService, Role.Manager).Show();
                        break;

                    case "3":
                        new EmployeeMenu(_employeeService, _requestService, Role.Developer).Show();
                        break;

                    case "4":
                        new EmployeeMenu(_employeeService, _requestService, Role.Tester).Show();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Input. Please try Again.");
                        break;
                }
            }
        }
    }
}
