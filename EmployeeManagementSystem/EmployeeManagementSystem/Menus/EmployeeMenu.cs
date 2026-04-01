using EmployeeManagementSystem.Entities;
//using EmployeeManagementSystem.Enums;
using EmployeeManagementSystem.Roles;
using EmployeeManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManagementSystem.Menus
{
    internal class EmployeeMenu
    {

        private readonly IEmployeeService _employeeService;
        private readonly RequestService _requestService;
        private readonly List<string> _capabilities = new List<string>();

        public EmployeeMenu(IEmployeeService employeeService, RequestService requestService)
        {
            _employeeService = employeeService;
            _requestService = requestService;
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
            Console.WriteLine($"Welcome {employee.Name}.");
            Console.WriteLine("Available Actions: ");

            //int optionNumber = 1;

            //var actions = new Dictionary<int, Action>();

            while (!logout)
            {
                int optionNumber = 1;
                var actions = new Dictionary<int, Action>();

                Console.WriteLine($"{optionNumber}. My Profile.");
                actions[optionNumber] = () => {
                    ShowProfile(employee);
                };
                optionNumber++;

                if (employee.HasRole<IApprover>())
                {
                    _capabilities.Add("CanApproveRequests");
                    Console.WriteLine($"{optionNumber}. View Requests.");
                    actions[optionNumber] = () =>
                    {
                        _requestService.DisplayRequests();
                    };
                    optionNumber++;
                }

                if (employee.HasRole<IApprover>())
                {
                    Console.WriteLine($"{optionNumber}. Approve/Reject Requests.");
                    actions[optionNumber] = () =>
                    {
                        ReqApproveOrReject(employee);
                    };
                    optionNumber++;
                }

                if (employee.HasRole<ITeamSupervisor>())
                {
                    _capabilities.Add("TeamSupervisor");
                    Console.WriteLine($"{optionNumber}. View Team Size.");

                    actions[optionNumber] = () =>
                    {
                        DisplayTeamSize(employee);
                    };
                    optionNumber++;
                }

                if (employee.HasRole<ICodeContributor>())
                {
                    _capabilities.Add("Developer");
                    Console.WriteLine($"{optionNumber}. Raise Request.");

                    actions[optionNumber] = () => {
                        RaiseRequest(employee);
                    };
                    optionNumber++;
                }

                if (employee.HasRole<ICodeContributor>())
                {
                    Console.WriteLine($"{optionNumber}. View My Requests.");

                    actions[optionNumber] = () =>
                    {
                        ViewMyRequests(employee);
                    };
                    optionNumber++;
                }

                Console.WriteLine("0. LogOut");

                Console.WriteLine("Choose Option: ");

                int choice = int.Parse(Console.ReadLine()!);

                if (actions.ContainsKey(choice))
                {
                    actions[choice].Invoke();
                }
                else if(choice == 0)
                {
                    Console.WriteLine("Lgging you Off...Done");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option.");
                }
            }
        }

        private Employee AuthenticateEmployee()
        {
            Console.WriteLine("Enter Employee ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
                return null;

            Employee employee = _employeeService.GetEmployeeById(id);
            Console.WriteLine("DEBUG: Roles loaded:");

            foreach (var role in employee.Roles)
            {
                Console.WriteLine(role.GetType().Name);
            }


            if (employee == null) return null;

            //role vadation part
            //if (_role == Role.Manager && employee is not Manager) return null;

            //if (_role == Role.Tester && employee is not Tester) return null;

            //if (_role == Role.Developer && employee is not Developer) return null;

            return employee;
        }

        private void ShowProfile(Employee employee)
        {
            Console.WriteLine("\n---My Profile---");
            Console.WriteLine($"ID: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Capabilities: {string.Join("," , _capabilities)}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Salary: {employee.Salary}");
        }

        private void RaiseRequest(Employee employee)
        {
            Console.WriteLine("Enter Request Message: ");
            string desc = Console.ReadLine();
            Request request = new Request(employee.Id, desc);
            _requestService.CreateRequest(request);
            Console.WriteLine("Request Raised Successfully.");
        }

        private void ViewMyRequests(Employee employee)
        {
            //Filtering the list using LINQ
            var reqs = _requestService.GetAllRequests().Where(r => r.EmployeeId == employee.Id);

            if (!reqs.Any()) //checks if the list has any items
            {
                Console.WriteLine("You didnt Raised any Requests.");
                return;
            }

            foreach(var request in reqs)
            {
                Console.WriteLine(
                    $"ID: {request.EmployeeId}, Name: {employee.Name}, Desc: {request.Description}, Status: {request.Status}"
                    );
            }
        }

        private void ReqApproveOrReject(Employee employee)
        {

            Console.WriteLine("Enter Request ID: ");
            int requestId = int.Parse(Console.ReadLine());
            Console.WriteLine("1. Approve");
            Console.WriteLine("2. Reject");
            Console.WriteLine("Enter your Choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _requestService.ApproveRequest(requestId, employee);
                    break;

                case "2":
                    _requestService.RejectRequest(requestId, employee);
                    break;
            }
        }

        private void DisplayTeamSize(Employee employee)
        {
            int teamSize = _employeeService.GetTeamSize(employee.Id);
            Console.WriteLine($"Your Team Size is: {teamSize}");
        }
    }
}
