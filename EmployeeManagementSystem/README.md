# Employee Management System (EMS)

## Overview

The **Employee Management System (EMS)** is a robust C# .NET Console Application built using a **Layered Architecture**. It facilitates the management of employees, roles, and requests through seamless integration with **SQL Server** using **Stored Procedures**.

This project serves as a comprehensive demonstration of backend development principles, including:

* **Layered Architecture:** Separation of concerns across Presentation, Service, and Data Access layers.
* **Database Integration:** Using SQL Server for persistent data storage.
* **Security & Performance:** Implementation of Stored Procedures to prevent SQL injection and optimize execution.
* **Logic Handling:** Role-based access control and business validation.

---

## Setup Instructions

### 1. Prerequisites

Ensure you have the following installed:

* **.NET SDK** (6.0 or later)
* **SQL Server** (Express or higher)
* **SQL Server Management Studio (SSMS)**

### 2. Clone the Repository

```bash
git clone https://github.com/Seenuvasan-Senthil/csharp-backend-foundations/tree/assessment2-adonet-emp-management
cd EmployeeManagementSystem

```

### 3. Database Configuration

1. Open **SQL Server Management Studio (SSMS)**.(I Used Windows authentication in ssms connection to SQL)
2. Execute the scripts located in the `/Database` folder in the following order:
* `01_Database.sql` (Creates the `EmployeeManagementSystemDB`)
* `02_Tables.sql` (Initializes required tables)
* `03_StoredProcedures.sql` (Registers the logic layer in the DB)



### 4. Configure the Connection String

Locate or create the `appsettings.json` file in the project root and add your SQL connection string.

**Example (SQL Server Authentication):**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EmployeeManagementSystemDB;User Id=appuser;Password=YourPassword;TrustServerCertificate=True;"
  }
}

```

> **Note:** For this project, it is recommended to create a dedicated SQL login under **Security** with `db_owner` permissions for the `EmployeeManagementSystemDB`.

### 5. Run the Application

You can run the project via the terminal:

```bash
dotnet build
dotnet run --project EMS

```

Alternatively, open the solution in **Visual Studio** and click the **Start/Play** icon.

---

## Architecture & Design

The system is built on a **Layered Architecture** to ensure maintainability and scalability:

  **Presentation** -> Manages user interaction and console input/output. 
  **Service** -> Handles business logic, data validation, and role-based rules. 
  **Data Access (DAL)** -> Manages communication with SQL Server. 
  **Database** -> Stores data and executes logic via Stored Procedures. 

___

## Features

* **Employee Management:** Create, update, and manage different employee types.
* **Role-Based Access:** Assign and retrieve specific roles and capabilities for employees.
* **Profile Management:** View detailed employee profiles governed by role-based logic.
* **Secure Data Handling:** Utilizes Stored Procedures instead of inline SQL to improve security and performance.

---

## Troubleshooting

### 1. SQL Server Authentication Failure

If you encounter a login failure while using SQL Server Authentication, ensure your SQL Server instance is set to **Mixed Mode**:

1. Open **SSMS** and right-click on your **Server Name** -> **Properties**.
2. Go to the **Security** page.
3. Under **Server authentication**, select **SQL Server and Windows Authentication mode**.
4. Restart the SQL Server service.

### 2. Connection String Issues

If the application cannot connect to the database:

* **Server Name:** Ensure `YOUR_SERVER_NAME` matches your actual SQL instance (e.g., `localhost` or `.\SQLEXPRESS`).
* **TrustServerCertificate:** If you are developing locally without an SSL certificate on your SQL Server, ensure `TrustServerCertificate=True;` is included in your connection string to avoid handshake errors.

### 3. Permission Errors

Ensure the `appuser` (or the login you created) has the `db_owner` role mapped specifically to the `EmployeeManagementSystemDB`.

---
