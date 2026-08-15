using System.Data.SqlTypes;
using Npgsql;
namespace InvoicingApp

{
    partial class Program
    {
        public static async Task CreateDatabase()
        {
            try
            {
                string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
                await using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();
                Console.WriteLine($"The PostgreSQL version: {conn.PostgreSqlVersion}");
                var sql = @"
            CREATE TABLE IF NOT EXISTS customers
            (
            id TEXT NOT NULL,
            name TEXT NOT NULL,
            email TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS products
            (
            id TEXT NOT NULL,
            productName TEXT NOT NULL,
            category TEXT NOT NULL,
            price DECIMAL NOT NULL
            );
            ";
                await using var cmd = new NpgsqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}