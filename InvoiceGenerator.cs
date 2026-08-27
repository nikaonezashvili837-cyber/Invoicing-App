namespace InvoicingApp
{
    public partial class Program
    {
        public async static Task InvoiceGenerator()
        {
            Console.WriteLine("--- Create Invoice ---");

            Console.Write("Customer Id: ");
            string customerId = Console.ReadLine()!;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Add Line Item ---");

                Console.Write("Item id: ");
                string itemId = Console.ReadLine()!;

                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine()!);

                Console.Write("Add another item? (Y/N): ");
                string? answer = Console.ReadLine();

                if (answer?.ToUpper() != "Y")
                {
                    break;
                }
            }

            Console.WriteLine();

            Console.Write("Discount (%): ");
            decimal discountPercent = decimal.Parse(Console.ReadLine()!);

            Console.Write("[1] Standard VAT (1%)  [2] Zero-rated : ");
            int vatType = int.Parse(Console.ReadLine()!);

            Console.Write("Due date (yyyy-MM-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine()!);

            Console.WriteLine();
            Console.WriteLine("--- Invoice Data Collected ---");

        }
    }
}