using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem.Data
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString =
    @"Server=APAC-FTDQ394\SQLEXPRESS;Database=EmployeeManagementSystemDB;User Id=appuser;Password=AppUser@123;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);

        }
    }
}
