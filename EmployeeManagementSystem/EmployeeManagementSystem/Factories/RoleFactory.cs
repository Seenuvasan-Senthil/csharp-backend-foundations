using EmployeeManagementSystem.Roles;

namespace EmployeeManagementSystem.Factories
{
    internal static class RoleFactory
    {
        public static IRole Create(string roleName)
        {
            return roleName switch
            {
                "Manager" => new ManagerRole(),
                "Developer" => new DeveloperRole(),
                "Tester" => new TesterRole(),
                _ => throw new Exception("Invalid role")
            };
        }
    }
}
