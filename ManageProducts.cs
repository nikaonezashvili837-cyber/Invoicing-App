using System.Runtime.InteropServices;

namespace InvoicingApp
{
    public partial class Program
    {
        public static void ManageProducts()
        {
            bool productMenu = true;
            // ProductManager productManager = new ProductManager();

            while (productMenu)
            {
                Console.Clear();

                Console.WriteLine("========== PRODUCT MENU ==========");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Edit Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. List Products");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add Product
                        break;

                    case "2":
                        // Edit Product
                        break;

                    case "3":
                        // Delete Product
                        break;

                    case "4":
                        // List Products
                        break;

                    case "5":
                        productMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}