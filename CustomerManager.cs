using System.Data.SqlTypes;
using Npgsql;
namespace InvoicingApp
{
    class CustomerManager()
    {
        public static async Task<List<Customer>> RetriveCustomers()
        {
            var sql = "SELECT id,name,email FROM customers";
            List<Customer> customers = new List<Customer>();
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
                    Customer customer = new Customer(id, name, email);
                    customers.Add(customer);
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
                await using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();
                await using var cmd = new NpgsqlCommand(sql, conn);
                if (name != null && email != null && id != null)
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@id", id);
                }
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed" + ex.Message);
            }
        }
        public static async Task CheckUserExistence(string? id)
        {
            List<Customer> customers =  await RetriveCustomers();
            bool customerExits = customers.Exists(el => el.Id == id);
            if(customerExits == false)
            {
                throw new Exception("Customer with such id Does not exist");
            };
        }
    }
}