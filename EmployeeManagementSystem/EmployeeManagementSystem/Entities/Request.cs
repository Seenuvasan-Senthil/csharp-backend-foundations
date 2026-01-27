using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagementSystem.Enums;

namespace EmployeeManagementSystem.Entities
{
    internal class Request
    {
        public int Id { get; private set; }
        public int EmployeeId { get; }
        public Role RequestedByRole { get; }
        public string Description { get; }
        public string Status { get; private set; }

        public Request(int empId, Role role, string description)
        {
            Id = empId;
            EmployeeId = empId;
            Description = description;
            Status = "Pending";
        }

        internal void SetId(int id) {
            if (Id != 0) throw new InvalidOperationException("Request Id Already Set.");
            Id = id;
        }

        internal void Approve()
        {
            Status = "Approved";
        }

        internal void Rejecte()
        {
            Status = "Rejected";
        }
    }
}
