using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem.Data
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString = ConfigurationHelper.GetConnectionString();

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);

        }
    }
}
