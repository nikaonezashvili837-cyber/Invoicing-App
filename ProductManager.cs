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
        public async Task<List<Product>> RetriveProducts()
        {
            List<Product> products = new List<Product>();
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
                        string Id = reader.GetString(0);
                        string Name = reader.GetString(1);
                        decimal Price = reader.GetDecimal(2);
                        Product product = new Product(Id, Name, Price);
                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return products;
        }
        public async void ListProducts()
        {
            List<Product> products = await RetriveProducts();
            foreach (Product product in products)
            {
                Console.WriteLine($"Id: {product.Id}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine("--------------------");
            }
        }
        public async Task EditTask(Product product)
        {
            try
            {
                var (id, productName, Price) = product;
                string? sql = @"
             UPDATE products
             SET
               productName = COALESCE(NULLIF(@productName, ''), productName),
               Price = COALESCE(@Price, Price)
             WHERE id = @id;
            ";
                await using var cmd = dataSource?.CreateCommand(sql);
                cmd?.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Text).Value = id;
                cmd?.Parameters.AddWithValue("ProductName", NpgsqlTypes.NpgsqlDbType.Text).Value = productName;
                cmd?.Parameters.AddWithValue("Price", NpgsqlTypes.NpgsqlDbType.Double).Value = Price;
                if (cmd != null)
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task DeleteProduct(String? id)
        {
            try
            {
                string sql = @"
                   DELETE FROM products
                   WHERE id = @id
                ";
                await using var cmd = dataSource?.CreateCommand(sql);
                cmd?.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Text).Value = id;
                if (cmd != null)
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}