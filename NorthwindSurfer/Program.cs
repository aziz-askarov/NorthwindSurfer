using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

//string connectionString = "Server=localhost;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;";
string connectionString = @"Server=.\SQLEXPRESS;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;";
IDbConnection dbConnection = new SqlConnection(connectionString);


while (true)
{
    while (true)
    {
        Console.Write("Mahsulot nomi: ");
        var input = Console.ReadLine();

        // Dapper uchun xavfsiz so'rov
        string sqlQuery = "SELECT ProductName FROM Products WHERE ProductName LIKE @SearchInput";

        try
        {
            // Parametrni obyekt sifatida uzatamiz: new { SearchInput = ... }
            var productNames = await dbConnection.QueryAsync<string>(sqlQuery, new { SearchInput = $"%{input}%" });

            foreach (var name in productNames)
            {
                Console.WriteLine(name);
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Xatolik: {ex.Message}");
        }

        Thread.Sleep(5000);
        Console.Clear();
    }

}

