using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1;

internal static class LocalDb
{
    internal const string DatabaseName = "WIM";

    private static string DbConnectionString =>
        $@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;TrustServerCertificate=True;Database={DatabaseName};";

    internal static int Execute(string sql, params SqlParameter[] parameters) =>
        ExecuteNonQuery(DbConnectionString, sql, parameters);

    internal static List<Product> GetProducts()
    {
        return Query(
            @"SELECT Id, Name, Quantity, Price, Category
              FROM dbo.Products
              ORDER BY Id ASC;",
            r => new Product
            {
                Id = r.GetInt32(0),
                Name = r.GetString(1),
                Quantity = r.GetInt32(2),
                Price = r.GetDecimal(3),
                Category = r.GetString(4),
            });
    }

    internal static int InsertProduct(Product p)
    {
        using var conn = new SqlConnection(DbConnectionString);
        using var cmd = new SqlCommand(@"
INSERT INTO dbo.Products (Name, Quantity, Price, Category)
VALUES (@Name, @Quantity, @Price, @Category);
SELECT CAST(SCOPE_IDENTITY() AS int);", conn);

        cmd.Parameters.Add(new SqlParameter("@Name", p.Name));
        cmd.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
        cmd.Parameters.Add(new SqlParameter("@Price", p.Price));
        cmd.Parameters.Add(new SqlParameter("@Category", p.Category));

        conn.Open();
        return (int)cmd.ExecuteScalar()!;
    }

    internal static void UpdateProduct(Product p)
    {
        Execute(
            @"UPDATE dbo.Products
              SET Name=@Name, Quantity=@Quantity, Price=@Price, Category=@Category
              WHERE Id=@Id;",
            new SqlParameter("@Id", p.Id),
            new SqlParameter("@Name", p.Name),
            new SqlParameter("@Quantity", p.Quantity),
            new SqlParameter("@Price", p.Price),
            new SqlParameter("@Category", p.Category));
    }

    internal static void DeleteProduct(int id)
    {
        Execute("DELETE FROM dbo.Products WHERE Id=@Id;", new SqlParameter("@Id", id));
    }

    internal static void ChangeQuantity(int id, int delta)
    {
        Execute(
            @"UPDATE dbo.Products
              SET Quantity = Quantity + @Delta
              WHERE Id = @Id;",
            new SqlParameter("@Id", id),
            new SqlParameter("@Delta", delta));
    }

    internal static T Scalar<T>(string sql, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(DbConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
        conn.Open();
        var value = cmd.ExecuteScalar();
        if (value is null || value is DBNull) return default!;
        return (T)Convert.ChangeType(value, typeof(T));
    }

    internal static List<T> Query<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(DbConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
        conn.Open();

        using var r = cmd.ExecuteReader();
        var list = new List<T>();
        while (r.Read()) list.Add(map(r));
        return list;
    }

    private static int ExecuteNonQuery(string connectionString, string sql, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(sql, conn);
        if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
        conn.Open();
        return cmd.ExecuteNonQuery();
    }
}

