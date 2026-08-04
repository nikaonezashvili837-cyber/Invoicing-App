using Npgsql;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
namespace InvoicingApp
{
    public partial class Program
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
                while(await reader.ReadAsync())
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
    }
}