
namespace InvoicingApp

{
    partial class Program
    {
        public static async Task Main()
        {
            bool isRunning = true;
            await CreateDatabase();
            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("========== INVOICING SYSTEM ==========");
                Console.WriteLine("1. Manage Customers");
                Console.WriteLine("2. Manage Products");
                Console.WriteLine("3. Create Invoice");
                Console.WriteLine("4. Record Payment");
                Console.WriteLine("5. View Invoice (with export)");
                Console.WriteLine("6. Reports");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ManageCustomers();
                        break;

                    case "2":
                        ManageProducts();
                        Console.WriteLine("Manage Products selected.");
                        break;

                    case "3":
                        Console.WriteLine("Create Invoice selected.");
                        break;

                    case "4":
                        Console.WriteLine("Record Payment selected.");
                        break;

                    case "5":
                        Console.WriteLine("View Invoice selected.");
                        break;

                    case "6":
                        Console.WriteLine("Reports selected.");
                        break;

                    case "7":
                        Console.WriteLine("Exiting...");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}