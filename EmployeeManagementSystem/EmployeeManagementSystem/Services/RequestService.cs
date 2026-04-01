using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Roles;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystem.Services
{
    internal class RequestService
    {
        public void CreateRequest(Request request)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var command = new SqlCommand("sp_CreateRequest", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
            command.Parameters.AddWithValue("@Description", request.Description);

            int newId = Convert.ToInt32(command.ExecuteScalar());

            request.SetId(newId);

            Console.WriteLine($"Request created with ID: {newId}");
        }

        public List<Request> GetAllRequests()
        {
            var list = new List<Request>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var command = new SqlCommand("sp_GetAllRequests", connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var req = new Request(
                    Convert.ToInt32(reader["EmployeeId"]),
                    reader["Description"].ToString()
                );

                req.SetId(Convert.ToInt32(reader["Id"]));

                if (reader["Status"].ToString() == "Approved")
                    req.Approve();
                else if (reader["Status"].ToString() == "Rejected")
                    req.Reject();

                list.Add(req);
            }

            return list;
        }

        public void DisplayRequests()
        {
            var requests = GetAllRequests();

            foreach (var request in requests)
            {
                Console.WriteLine(
                    $"ReqID: {request.Id}, EmpID: {request.EmployeeId}, Desc: {request.Description}, Status: {request.Status}");
            }
        }

        public void ApproveRequest(int requestId, Employee approver)
        {
            if (!approver.HasRole<IApprover>())
            {
                Console.WriteLine("Not authorized.");
                return;
            }

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var command = new SqlCommand("sp_ApproveRequest", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", requestId);

            command.ExecuteNonQuery();

            Console.WriteLine("Request approved.");
        }

        public void RejectRequest(int requestId, Employee approver)
        {
            if (!approver.HasRole<IApprover>())
            {
                Console.WriteLine("Not authorized.");
                return;
            }

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var command = new SqlCommand("sp_RejectRequest", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", requestId);

            command.ExecuteNonQuery();

            Console.WriteLine("Request rejected.");
        }
    }
}
