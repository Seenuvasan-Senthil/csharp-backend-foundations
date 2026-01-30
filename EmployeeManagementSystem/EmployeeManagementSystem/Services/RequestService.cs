using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Roles;

namespace EmployeeManagementSystem.Services
{
    internal class RequestService //request can be raised by any employee
    {
        private readonly List<Request> _requests = new();
        private int _nextId = 1; //id is assigned only by this service

        public void CreateRequest(Request request) //public constructor-anyone can initialize
        {
            request.SetId(_nextId++); //request object property
            _requests.Add(request);
        }

        //helper methods 

        public List<Request> GetAllRequests()
        {
            return _requests;
        }

        public void DisplayRequests()
        {
            _requests.ForEach(request => {
                Console.WriteLine($"ReqID: {request.Id}, EmpID: {request.EmployeeId}, Desc: {request.Description}, Status: {request.Status}");
            });
        }

        public Request? GetRequestById(int id)
        {
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public void ApproveRequest(int requestId, Employee approver)
        {
            if (!approver.HasRole<IApprover>())
            {
                Console.WriteLine("You are not authorized to Approve");
                return;
            }
            Request request = GetRequestById(requestId);
            if(request == null)
            {
                Console.WriteLine("No Requests was/were raised under this ID");
                return;
            }
            request.Approve();
            Console.WriteLine($"{requestId} is Approved.");
        }

        public void RejectRequest(int requestId, Employee approver)
        {
            if (!approver.HasRole<IApprover>())
            {
                Console.WriteLine("You are not authorized to Reject.");
                return;
            }
            Request request = GetRequestById(requestId);
            if (request == null)
            {
                Console.WriteLine("No Requests was/were raised under this ID");
                return;
            }
            request.Reject();
            Console.WriteLine($"{requestId} is Rejected.");
        }
    }
}
