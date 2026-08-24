using System;
namespace InvoicingApp
{
    public partial class Program
    {
        
        public static async Task ManageCustomers()
        {
            bool customerMenu = true;
            CustomerManager CustomerManager = new CustomerManager();
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
                        Guid uniqueId = Guid.NewGuid();
                        string idString = uniqueId.ToString();
                        Console.Write("Enter name: ");
                        string? userName = Console.ReadLine();
                        Console.Write("Enter email: ");
                        string? userEmail = Console.ReadLine();
                        await CustomerManager.AddCustomers(idString, userName, userEmail);
                        break;

                    case "2":
                        Console.Write("Enter id: ");
                        string? id = Console.ReadLine();
                        try
                        {
                            await CustomerManager.CheckUserExistence(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        Console.WriteLine("Enter the new name");
                        string? name = Console.ReadLine();
                        Console.WriteLine("Enter the new email");
                        string? email = Console.ReadLine();
                        Customer c = new Customer(id, name, email);
                        await CustomerManager.EditCustomers(c);
                        Console.WriteLine("Edit Customer selected.");
                        break;

                    case "3":
                        Console.Write("Enter id: ");
                        id = Console.ReadLine();
                        List<Customer> customers = await CustomerManager.RetriveCustomers();
                        await CustomerManager.DeleteCustomers(customers.Find(el => el.Id == id));
                        Console.WriteLine("Delete Customer selected.");
                        break;

                    case "4":
                        await CustomerManager.ListCustomers();
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