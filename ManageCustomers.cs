namespace InvoicingApp
{
    public partial class Program
    {
        public static void ManageCustomers()
        {
            bool customerMenu = true;

            while (customerMenu)
            {
                Console.Clear();

                Console.WriteLine("========== CUSTOMER MENU ==========");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Edit Customer");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. List Customers");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add Customer selected.");
                        break;

                    case "2":
                        Console.WriteLine("Edit Customer selected.");
                        break;

                    case "3":
                        Console.WriteLine("Delete Customer selected.");
                        break;

                    case "4":
                        Console.WriteLine("List Customers selected.");
                        break;

                    case "5":
                        Console.WriteLine("Returning to Main Menu...");
                        customerMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (customerMenu)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}