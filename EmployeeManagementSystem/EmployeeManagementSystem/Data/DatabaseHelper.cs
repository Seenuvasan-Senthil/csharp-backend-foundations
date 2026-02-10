using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem.Data
{
    public static class DatabaseHelper
    {
        private static string ConnectionString =
            @"Server=localhost\SQLEXPRESS, Database=EmployeeManagementSystemDB; Trusted_Connection=true; Trust_Server_Certificate=true;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);

        }
    }
}
