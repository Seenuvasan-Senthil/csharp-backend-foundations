using EmployeeManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Employee");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Select your Role: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new AdminMenu(_employeeService, _requestService).Show();
                        break;

                    case "2":
                        new EmployeeMenu(_employeeService, _requestService).Show();
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
