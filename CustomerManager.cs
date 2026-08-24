using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Npgsql;
namespace InvoicingApp
{
    class CustomerManager : CrudManager
    {
        public static async Task<List<Customer>> RetriveCustomers()
        {
            var sql = "SELECT id,name,email FROM customers";
            List<Customer> customers = new List<Customer>();

            try
            {
                await using var cmd = dataSource?.CreateCommand(sql);
                if (cmd != null)
                {
                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var id = reader.GetString(0);
                        var name = reader.GetString(1);
                        var email = reader.GetString(2);
                        Customer customer = new Customer(id, name, email);
                        customers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }

        public static async Task ListCustomers()
        {
            List<Customer> customers = await RetriveCustomers();
            foreach (Customer c in customers)
            {
                Console.WriteLine($"{c.Id}\t{c.Name}\t{c.Email}");
            }
        }
        public static async Task AddCustomers(string id, string? name, string? email)
        {
            try
            {
                string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
                string sql = @"
            INSERT INTO customers (id, name, email)
            VALUES (@id, @name, @email)";
                // await using var dataSource = NpgsqlDataSource.Create(connectionString);
                await using var cmd = dataSource?.CreateCommand(sql);
                cmd?.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Text).Value = id;
                cmd?.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Text).Value = name;
                cmd?.Parameters.AddWithValue("email", NpgsqlTypes.NpgsqlDbType.Text).Value = email;
                if (cmd != null)
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                Console.WriteLine("Hurray");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task EditCustomers(Customer c)
        {
            try
            {
                var (id, name, email) = c;
                string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
                var sql = @"
            UPDATE customers
            SET 
              name = COALESCE(NULLIF(@name, ''), name),
              email = COALESCE(NULLIF(@email, ''), email)
            WHERE id = @id;
           ";
                await using var cmd = dataSource?.CreateCommand(sql);
                cmd?.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Text).Value = id;
                cmd?.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Text).Value = name;
                cmd?.Parameters.AddWithValue("email", NpgsqlTypes.NpgsqlDbType.Text).Value = email;
                if (cmd != null)
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed" + ex.Message);
            }
        }
        public static async Task DeleteCustomers(Customer? c)
        {
            try
            {
                if (c == null)
                {
                    Console.WriteLine("no such customer exists");
                    return;
                }
                string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
                var id = c?.Id;
                var sql = @"
              DELETE FROM customers
              WHERE id = @id
            ";
                await using var dataSource = NpgsqlDataSource.Create(connectionString);
                await using var cmd = dataSource.CreateCommand(sql);
                if (id != null)
                {
                    cmd.Parameters.AddWithValue("id", id);
                }
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task CheckUserExistence(string? id)
        {
            List<Customer> customers = await RetriveCustomers();
            bool customerExits = customers.Exists(el => el.Id == id);
            if (customerExits == false)
            {
                throw new Exception("Customer with such id Does not exist");
            }
            ;
        }
    }
}