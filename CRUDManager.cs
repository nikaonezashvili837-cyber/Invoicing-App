using Npgsql;
namespace InvoicingApp
{
    class CrudManager
    {
        protected static NpgsqlDataSource? dataSource;
        public CrudManager()
        {
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            dataSource = NpgsqlDataSource.Create(connectionString);
        }
    }
}