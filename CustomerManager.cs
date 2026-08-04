using System.Data.SqlTypes;
using Npgsql;
namespace InvoicingApp
{
    class CustomerManager()
    {
        public static async Task ListCustomers()
        {
            var sql = "SELECT id,name,email FROM customers";
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            try
            {
                await using var dataSource = NpgsqlDataSource.Create(connectionString);
                await using var cmd = dataSource.CreateCommand(sql);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var id = reader.GetString(0);
                    var name = reader.GetString(1);
                    var email = reader.GetString(2);
                    Console.WriteLine($"{id}\t{name}\t{email}");
                }
            }
            catch (Exception ex)
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
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("name", name ?? "");
            cmd.Parameters.AddWithValue("email", email ?? "");
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine("Hurray");
        }
    }
}