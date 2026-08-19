using Npgsql;

namespace InvoicingApp
{
    class ProductManager : CrudManager
    {
        public async Task AddProducts(Product p)
        {
            var (id, productName,  price) = p;

            string sql = @"INSERT INTO products (id,productName,price) 
            VALUES(@id,@productName,@price)";
            Console.WriteLine(id);
            Console.WriteLine(productName);
            Console.WriteLine(price);
            if (dataSource != null && id != null && productName != null  && price != null)
            {
                Console.WriteLine("i am here");
                await using var cmd = dataSource.CreateCommand(sql);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("productName", productName);
                cmd.Parameters.AddWithValue("price", price);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}