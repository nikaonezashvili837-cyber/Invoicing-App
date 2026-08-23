using Npgsql;

namespace InvoicingApp
{
    class ProductManager : CrudManager
    {
        public async Task AddProducts(Product p)
        {
            try
            {
                var (id, productName, price) = p;

                string sql = @"INSERT INTO products (id,productName,price) 
            VALUES(@id,@productName,@price)";
                Console.WriteLine(id);
                Console.WriteLine(productName);
                Console.WriteLine(price);
                if (dataSource != null && id != null && productName != null && price != null)
                {
                    Console.WriteLine("i am here");
                    await using var cmd = dataSource.CreateCommand(sql);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("productName", productName);
                    cmd.Parameters.AddWithValue("price", price);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task RetriveProducts()
        {
            try
            {
                string sql = "SELECT * FROM products";
                await using var cmd = dataSource?.CreateCommand(sql);
                if (cmd != null)
                {
                    await using var reader = await cmd.ExecuteReaderAsync();
                    Console.WriteLine("retrived");
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine($"Id: {reader.GetString(0)}");
                        Console.WriteLine($"Name: {reader.GetString(1)}");
                        Console.WriteLine($"Price: {reader.GetDecimal(2):C}");
                        Console.WriteLine("--------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}