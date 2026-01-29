using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Roles
{
    public class ManagerRole : IApprover, ITeamSupervisor
    {
        public string RoleName => "Manager";

        public void Approve(int requestId)
        {
            Console.WriteLine($"Manager was approved: {requestId}");
        }

        public int GetTeamSize(int mangerId)
        {
            return 0;
            //return _employeeService.GetTeamSize(mangerId);
        }
    }
}
