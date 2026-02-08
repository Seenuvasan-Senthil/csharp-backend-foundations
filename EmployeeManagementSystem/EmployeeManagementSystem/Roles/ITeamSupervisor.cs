using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Roles
{
    public interface ITeamSupervisor : IRole
    {
        int GetTeamSize(int managerId);
    }
}
