# C# to SQL Connector Project

This project is a C# library that provides a simple and efficient way to connect to a SQL Server database and execute various database operations. The code includes a `SqlDBInstance` class that encapsulates the functionality for establishing a connection, executing SQL commands, and retrieving data from the database.

## Features

- **Connection Management**: Establishes a connection to the SQL Server database using the provided connection string.
- **SQL Command Execution**: Executes SQL commands such as INSERT, UPDATE, DELETE, and SELECT statements.
- **Stored Procedure Support**: Supports the execution of stored procedures with parameters and returns values.
- **Data Retrieval**: Retrieves data from the database and returns the result as a DataTable.
- **Data Insertion**: Allows insertion of data from a DataTable into the SQL Server database.
- **Error Handling**: Provides exception handling for database-related exceptions.

## Usage

1. Add the `STOCK_SYS` namespace to your C# project.

2. Create an instance of the `SqlDBInstance` class to establish a connection to the SQL Server database.

```csharp
SqlDBInstance dbInstance = new SqlDBInstance();
```

3. Use the various methods provided by the `SqlDBInstance` class to execute SQL commands and retrieve data from the database. For example, to execute a SQL command:

```csharp
int rowsAffected = dbInstance.generateCommand("INSERT INTO TableName (Column1, Column2) VALUES (@Value1, @Value2)", CommandType.Text);
```

4. To execute a stored procedure with parameters:

```csharp
Hashtable parameters = new Hashtable();
parameters.Add("Param1", value1);
parameters.Add("Param2", value2);

int returnValue = dbInstance.generateCommand("StoredProcedureName", parameters);
```

5. To retrieve data from the database and get the result as a DataTable:

```csharp
DataTable dataTable = dbInstance.getDataTable("SELECT * FROM TableName");
```

6. To insert data from a DataTable into the SQL Server database:

```csharp
DataTable dataTable = GetDataTableToInsert();

int rowsInserted = dbInstance.generateCommand("StoredProcedureName", "ParameterName", dataTable);
```

7. Remember to handle exceptions when using the methods provided by the `SqlDBInstance` class to ensure proper error handling and cleanup.

## Configuration

To configure the SQL Server connection, update the `getConnectionString()` method in the `SqlDBInstance` class with the appropriate connection string. Modify the following line:

```csharp
return "Data Source=RAHUL-PC\\SQLEXPRESS;Initial Catalog=STOCK_SYS;Integrated Security=True";
```

Replace the connection string with your SQL Server details.

## Compatibility

This code is compatible with SQL Server databases and has been tested with the Microsoft SQL Server database.

## Contact

For any inquiries or support, please contact the project owner:

- Name: [Shweta Kumavat]
- Email: [shvetakumavat137.055@gmail.com]
