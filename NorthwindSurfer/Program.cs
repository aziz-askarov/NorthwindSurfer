using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

string connectionString = "Server=localhost;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;";

IDbConnection dbConnection = new SqlConnection(connectionString);

string sqlQuery = "select TOP(1) productid || ' - ' || productname from products order by productid;";

var productNames = await dbConnection.QueryAsync<string>(sqlQuery);

//foreach(var productName in productNames)
//    Console.WriteLine(productName);

var name = await dbConnection.QueryFirstOrDefaultAsync<string>(sqlQuery);
Console.WriteLine(name);

Console.Write("Product id: ");
var productId = int.Parse(Console.ReadLine());

dynamic product = await dbConnection.QueryFirstOrDefaultAsync(
    "select * from products where ProductId = @productId",
    new { productId });


Console.WriteLine($"{product.ProductId}");
Console.WriteLine($"{product.ProductName}");
Console.WriteLine($"{product.CategoryID}");
Console.WriteLine($"{product.UnitPrice}");

