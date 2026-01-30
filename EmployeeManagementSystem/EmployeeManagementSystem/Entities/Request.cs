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
        public string Description { get; }
        public bool IsApproved { get; private set; }
        public bool IsRejected { get; private set; }
        public Role RequestedByRole { get; }
        public string Status { get; private set; }


        public Request(int empId, string description)
        {
            EmployeeId = empId;
            Description = description;
            Status = "Pending";
        }

        internal void SetId(int id) {
            if (Id != 0) throw new InvalidOperationException("Request Id Already Set.");
            Id = id;
        }

        public void Approve() => IsApproved = true;

        public void Reject() => IsRejected = true;

        //internal void Approve()
        //{
        //    Status = "Approved";
        //}

        //internal void Reject()
        //{
        //    Status = "Rejected";
        //}
    }
}
