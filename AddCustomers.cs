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
            )";
                await using var cmd = new NpgsqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task AddCustomers(string id, string? name, string? email)
        {
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            string sql = @"
            INSERT INTO customers (id, name, email)
            VALUES (@id, @name, @email)";
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id",id);
            cmd.Parameters.AddWithValue("name",name ?? "");
            cmd.Parameters.AddWithValue("email",email ?? "");
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine("Hurray");
        }
    }
}