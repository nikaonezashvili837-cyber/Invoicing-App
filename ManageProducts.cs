using System.Runtime.InteropServices;
using System.Security.Authentication;
namespace InvoicingApp
{
    public partial class Program
    {
        public static async Task ManageProducts()
        {
            bool productMenu = true;
            ProductManager productManager = new ProductManager();

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
                        Console.Write("Enter product name: ");
                        string productName = Console.ReadLine()!;

                        Console.Write("Enter product price: ");
                        decimal price = decimal.Parse(Console.ReadLine()!);
                        Guid guid = Guid.NewGuid();
                        string Id = guid.ToString();
                        Product product = new Product(Id, productName, price);
                        await productManager.AddProducts(product);
                        break;

                    case "2":
                        Console.WriteLine("Enter product id to edit");
                        string? id = Console.ReadLine();
                        Console.WriteLine("Enter edited name");
                        string? name = Console.ReadLine();
                        Console.WriteLine("Enter edited price");
                        string? entredPrice = Console.ReadLine();
                        if(decimal.TryParse(entredPrice, out decimal result))
                        {
                            Product editedProduct = new Product(id,name,result);
                            await productManager.EditTask(editedProduct);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                        }
                        break;

                    case "3":
                        // Delete Product
                        break;

                    case "4":
                        await productManager.RetriveProducts();

                        // List Products
                        break;

                    case "5":
                        productMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                if (productMenu)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }

        }
    }
}