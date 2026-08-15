using Npgsql;

namespace InvoicingApp
{
    class ProductManager
    {
        private static NpgsqlDataSource? dataSource;
        public ProductManager()
        {
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            dataSource = NpgsqlDataSource.Create(connectionString);
        }
    }
}