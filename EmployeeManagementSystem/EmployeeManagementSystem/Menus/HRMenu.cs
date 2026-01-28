using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Entities;
using System;
using System.Net.Security;

namespace EmployeeManagementSystem.Menus
{
    internal class HRMenu
    {
        private const int HR_SECRET_KEY = 10;
        private readonly IEmployeeService _employeeService;
        private readonly RequestService _requestService;

        public HRMenu(IEmployeeService employeeService, RequestService requestService)
        {
            _employeeService = employeeService;
            _requestService = requestService;
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
                Console.WriteLine("5. View Employee Requests");
                Console.WriteLine("6. Approve Emplyee Request");
                Console.WriteLine("7. Reject Employee Request");
                Console.WriteLine("0. Logout");
                Console.WriteLine("Choose Option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployeeFlow();
                        break;

                    case "2":
                        RemoveEmployeeFlow();
                        break;

                    case "3":
                        ViewAllEmployees();
                        break;

                    case "4":
                        UpdateSalaryFlow();
                        break;

                    case "5":
                        DisplayAllRequests();
                        break;

                    case "6":
                        ApproveRequest();
                        break;

                    case "7":
                        RejectRequest();
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

        //private helpers, private-only to HR

        private bool Authenticate()
        {
            Console.WriteLine("Enter Hr Secret Key: ");
            Console.WriteLine("Please enter 10");
            return int.TryParse(Console.ReadLine(), out int key) && key == HR_SECRET_KEY; 
        }

        private void AddEmployeeFlow()
        {
            Console.WriteLine("\nSelect Employee Type:");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Tester");
            Console.WriteLine("Enter your Choice: ");

            string typeChoice = Console.ReadLine();

            Console.WriteLine("Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Department:");
            string department = Console.ReadLine();

            Console.WriteLine("Salary:");
            //we declare "salary" right inside the TryParse()
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary)) {
                Console.WriteLine("Invalid argument type.");
                return;
            }


            Employee employee = typeChoice switch
            {
                "1" => CreateManager(name, department, salary),
                "2" => CreateTester(name, department, salary),
                _ => null
            };

            if(employee == null)
            {
                Console.WriteLine("Invalid Employee Type");
                return;
            }

            _employeeService.AddEmployee(employee);
            Console.WriteLine("Employee Added Sucess fully");
        }

        private Employee CreateManager(string name, string department, decimal salary)
        {
            Console.WriteLine("Enter TeamSize: ");
            if(!int.TryParse(Console.ReadLine(), out int teamSize))
            {
                Console.WriteLine("Please enter a Valid number. Try Again.");
            }
            //int teamSize = int.Parse(Console.ReadLine());
            return new Manager(name, department, salary, teamSize);
        }

        private Employee CreateTester(string name, string department, decimal salary)
        {
            Console.WriteLine("TechStack: ");
            string techStack = Console.ReadLine();
            return new Tester(name, department, salary, techStack);
        }

        private void RemoveEmployeeFlow()
        {
            Console.WriteLine("Enter Employee ID: ");
            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID Format Exception.");
                return;
            }

            _employeeService.RemoveEmployee(id);
            Console.WriteLine("Employee Removed Success");
        }

        private void ViewAllEmployees()
        {
            Console.WriteLine("\n---Employee List---");

            foreach(var emp in _employeeService.GetAllEmployees())
            {
                Console.WriteLine(
                    $"ID: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}, Salary: {emp.Salary}");
            }
        }

        private void UpdateSalaryFlow()
        {
            Console.Write("Enter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID Format Exception.");
                return;
            }
            //int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new Salary: ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal salary))
            {
                Console.WriteLine("Please enter a Valid Number.");
                return;
            }

            _employeeService.UpdateSalary(id, salary);
            Console.WriteLine("Salary updation Success.");
        }

        private void DisplayAllRequests()
        {
            foreach(var r in _requestService.GetAllRequests())
            {
                Console.WriteLine(
                    $"RequestID: {r.Id} | EmpId: {r.EmployeeId} | Role: {r.RequestedByRole} | Desc: {r.Description} |Status: {r.Status}"
                    );
            }
        }

        private void ApproveRequest()
        {
            int id = IsFound();
            if(id != 0)
            {
                _requestService.ApproveRequest(id);
                Console.WriteLine("Approved");
            }
        }

        private void RejectRequest()
        {
            int val = IsFound();
            if (val != 0)
            {
                _requestService.RejectRequest(val);
                Console.WriteLine("Rejected");
            }
        }

        private int IsFound()
        {
            Console.WriteLine("Enter Request ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("NumberFormat Exception.");
                return 0;
            }
            var req = _requestService.GetById(id);
            if (req == null)
            {
                Console.WriteLine("no requests raised under this ID.");
                return 0;
            }

            return id;
        }
    }
}
