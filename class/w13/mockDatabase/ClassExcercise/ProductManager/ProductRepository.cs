namespace ProductManager;

using Npgsql;
using System.Collections.Generic;
using System.Data;

public class ProductRepository
{
    private readonly IDbConnection _connection;

    public ProductRepository()
    {
        _connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres");
    }

    public ProductRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public List<Product> GetProductsByCategory(string category)
    {
        var products = new List<Product>();

        _connection.Open();

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT name, category, price FROM products WHERE category = '" + category + "'";
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Name = reader.GetString(0),
                Category = reader.GetString(1),
                Price = reader.GetString(2)
            });
        }

        _connection.Close();

        return products;
    }
}
