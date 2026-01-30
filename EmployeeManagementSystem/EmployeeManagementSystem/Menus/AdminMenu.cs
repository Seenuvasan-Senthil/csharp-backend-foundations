using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Entities;
using System;
using System.Net.Security;
using EmployeeManagementSystem.Roles;

namespace EmployeeManagementSystem.Menus
{
    internal class AdminMenu
    {
        private const int HR_SECRET_KEY = 10;
        private readonly IEmployeeService _employeeService;
        private readonly RequestService _requestService;

        public AdminMenu(IEmployeeService employeeService, RequestService requestService)
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
                Console.WriteLine("\n---Admin Menu---");
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

                    //case "6":
                    //    ApproveRequest();
                    //    break;

                    //case "7":
                    //    RejectRequest();
                    //    break;

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
            Console.WriteLine("Enter Admin Secret Key: ");
            Console.WriteLine("Please enter 10");
            return int.TryParse(Console.ReadLine(), out int key) && key == HR_SECRET_KEY; 
        }

        private void AddEmployeeFlow()
        {
            //Console.WriteLine("\nSelect Employee Type:");
            //Console.WriteLine("1. Manager");
            //Console.WriteLine("2. Tester");
            //Console.WriteLine("3. Developer");
            //Console.WriteLine("Enter your Choice: ");

            //string typeChoice = Console.ReadLine();

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

            Console.WriteLine("Enter Manager ID(or 0 if none): ");
            int managerId = int.Parse(Console.ReadLine());

            Employee employee = new PermanentEmployee(name, department, salary);

            if(managerId != 0)
            {
                employee.AssignManager(managerId);
                var man = _employeeService.GetEmployeeById(managerId);
                string manName = man.Name;
                Console.WriteLine($"Your Reporting Manager is :{manName}");
            }

            //Employee employee1 = typeChoice switch
            //{
            //    "1" => CreateManager(name, department, salary),
            //    "2" => CreateTester(name, department, salary),
            //    "3" => CreateDeveloper(name, department, salary),
            //    _ => null
            //};

            bool addingRoles = true;

            while (addingRoles)
            {
                Console.WriteLine("\nAssign Role: ");
                Console.WriteLine("1. Manager");
                Console.WriteLine("2. Tester");
                Console.WriteLine("3. Developer");
                Console.WriteLine("0. Done");

                string roleChoice = Console.ReadLine();

                switch (roleChoice)
                {
                    case "1":
                        employee.AssignRole(new ManagerRole());
                        Console.WriteLine("Manager Role Assigned.");
                        break;

                    case "2":
                        employee.AssignRole(new TesterRole());
                        Console.WriteLine("Tester Role Assigned.");
                        break;

                    case "3":
                        employee.AssignRole(new DeveloperRole());
                        Console.WriteLine("Developer Role Assigned.");
                        break;

                    case "0":
                        addingRoles = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Role Choice.");
                        break;
                }
            }

            if (employee == null)
            {
                Console.WriteLine("Invalid Employee Type");
                return;
            }

            _employeeService.AddEmployee(employee);
            Console.WriteLine("Employee Added Sucess fully");
        }

        private Employee CreateManager(string name, string department, decimal salary) //manager constructor called
        {
            Console.WriteLine("Enter TeamSize: ");
            if(!int.TryParse(Console.ReadLine(), out int teamSize))
            {
                Console.WriteLine("Please enter a Valid number. Try Again.");
            }
            //int teamSize = int.Parse(Console.ReadLine());
            return new Manager(name, department, salary, teamSize);
        }

        private Employee CreateTester(string name, string department, decimal salary) //tester constructor called
        {
            Console.WriteLine("TechStack: ");
            string techStack = Console.ReadLine();
            return new Tester(name, department, salary, techStack);
        }

        private Employee CreateDeveloper(string name, string department, decimal salary) //developer constructor called
        {
            Console.WriteLine("Area:");
            string area = Console.ReadLine();
            return new Developer(name, department, salary, area);
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

        //private void ApproveRequest()
        //{
        //    int id = IsFound();
        //    if(id != 0)
        //    {
        //        _requestService.ApproveRequest(id);
        //        Console.WriteLine("Approved");
        //    }
        //}

        //private void RejectRequest()
        //{
        //    int val = IsFound();
        //    if (val != 0)
        //    {
        //        _requestService.RejectRequest(val);
        //        Console.WriteLine("Rejected");
        //    }
        //}

        //private int IsFound()
        //{
        //    Console.WriteLine("Enter Request ID: ");
        //    if (!int.TryParse(Console.ReadLine(), out int id))
        //    {
        //        Console.WriteLine("NumberFormat Exception.");
        //        return 0;
        //    }
        //    var req = _requestService.GetById(id);
        //    if (req == null)
        //    {
        //        Console.WriteLine("no requests raised under this ID.");
        //        return 0;
        //    }

        //    return id;
        //}
    }
}
