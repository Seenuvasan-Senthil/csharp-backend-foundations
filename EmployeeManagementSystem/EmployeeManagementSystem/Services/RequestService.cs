using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Services
{
    internal class RequestService
    {
        private readonly List<Request> _requests = new();
        private int _nextId = 1;

        public void CreateRequest(Request request)
        {
            request.SetId(_nextId++);
            _requests.Add(request);
        }

        public List<Request> GetAllRequests()
        {
            return _requests;
        }

        public Request? GetById(int id)
        {
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public void ApproveRequest(int id)
        {
            var request = GetById(id);
            request?.Approve();
        }

        public void RejectRequest(int id)
        {
            var request = GetById(id);
            request?.Rejecte();
        }
    }
}
