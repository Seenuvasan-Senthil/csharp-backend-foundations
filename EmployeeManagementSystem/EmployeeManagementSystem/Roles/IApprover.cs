using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Roles
{
    public interface IApprover : IRole
    {
        void Approve(int requestId);
    }
}
