using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using EmployeeManagementSystem.Factories;

public class EmployeeService : IEmployeeService
{
    public void AddEmployee(Employee employee)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        using var command = new SqlCommand("sp_AddEmployee", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Name", employee.Name);
        command.Parameters.AddWithValue("@Department", employee.Department);
        command.Parameters.AddWithValue("@Salary", employee.Salary);
        command.Parameters.AddWithValue("@ManagerId",
            (object?)employee.ManagerId ?? DBNull.Value);

        int newId = Convert.ToInt32(command.ExecuteScalar());

        employee.SetId(newId);

        Console.WriteLine($"Employee saved to DB with ID: {newId}");
    }

    public Employee GetEmployeeById(int id)
    {
        Employee employee = null;

        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        // Get Employee
        using (var command = new SqlCommand("sp_GetEmployeeById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = new PermanentEmployee(
                    reader["Name"].ToString(),
                    reader["Department"].ToString(),
                    Convert.ToDecimal(reader["Salary"])
                );

                employee.SetId(Convert.ToInt32(reader["Id"]));

                if (reader["ManagerId"] != DBNull.Value)
                    employee.AssignManager(Convert.ToInt32(reader["ManagerId"]));
            }
        }

        if (employee == null)
            return null;

        // Load Roles
        using (var command = new SqlCommand("sp_GetRolesByEmployeeId", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmployeeId", id);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var roleName = reader["CapabilityName"].ToString();

                var role = RoleFactory.Create(roleName);

                employee.AssignRole(role);
            }
        }

        return employee;
    }

    public void AddCapabilityToEmployee(int employeeId, int capabilityId)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        using var command = new SqlCommand("sp_AddCapabilityToEmployee", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@EmployeeId", employeeId);
        command.Parameters.AddWithValue("@CapabilityId", capabilityId);

        command.ExecuteNonQuery();
    }



    public List<Employee> GetAllEmployees()
    {
        var employees = new List<Employee>();

        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        using var command = new SqlCommand("sp_GetAllEmployees", connection);
        command.CommandType = CommandType.StoredProcedure;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var employee = new PermanentEmployee(
                reader["Name"].ToString(),
                reader["Department"].ToString(),
                Convert.ToDecimal(reader["Salary"])
            );

            employee.SetId(Convert.ToInt32(reader["Id"]));

            employees.Add(employee);
        }

        return employees;
    }

    public void UpdateSalary(int id, decimal newSalary)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        using var command = new SqlCommand("sp_UpdateSalary", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Salary", newSalary);

        command.ExecuteNonQuery();

        Console.WriteLine("Salary updated in DB.");
    }

    public void RemoveEmployee(int id)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        using var command = new SqlCommand("sp_DeleteEmployee", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();

        Console.WriteLine("Employee deleted from DB.");
    }

    public int GetTeamSize(int managerId)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string query = "SELECT COUNT(*) FROM Employees WHERE ManagerId = @ManagerId";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@ManagerId", managerId);

        return (int)command.ExecuteScalar();
    }
}
