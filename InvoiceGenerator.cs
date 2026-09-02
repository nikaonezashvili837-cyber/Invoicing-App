namespace InvoicingApp
{
    public partial class Program
    {

        public async static Task InvoiceGenerator()
        {
            try
            {
                Console.WriteLine("--- Create Invoice ---");
                CustomerManager customerManager = new CustomerManager();
                ProductManager productManager = new ProductManager();
                Console.Write("Customer Id: ");
                string customerId = Console.ReadLine()!;
                List<Customer> customers = await CustomerManager.RetriveCustomers();
                List<Product> products = await productManager.RetriveProducts();
                Customer? customer = customers.Find(customer => customer.Id == customerId);
                Invoice invoice = new Invoice();
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("--- Add Line Item ---");

                    Console.Write("Item id: ");
                    string productId = Console.ReadLine()!;
                    Product? product = products.Find(product => product.Id == productId);

                    Console.Write("Quantity: ");
                    int quantity = int.Parse(Console.ReadLine()!);
                    SelectedProductData selectedProduct = new SelectedProductData(product, quantity);
                    invoice.AddProduct(selectedProduct);
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

                Console.Write("[1] Standard VAT (18%)  [2] Zero-rated : ");
                int vatType = int.Parse(Console.ReadLine()!);

                Console.Write("Due date (yyyy-MM-dd): ");
                DateTime dueDate = DateTime.Parse(Console.ReadLine()!);

                Console.WriteLine();
                Console.WriteLine("--- Invoice Data Collected ---");
                List<SelectedProductData> selectedProducts = invoice.RetriveProducts();
                string lineItems = "";
                decimal subtotal = 0;
                foreach (SelectedProductData selectedProduct in selectedProducts)
                {
                    string? name = selectedProduct.ProductName;
                    int amount = selectedProduct.Amount;
                    decimal price = selectedProduct.SelectedProduct.Price;
                    decimal total = price * amount;
                    string lineItem = $" {name} - X{amount} @ ${price} = ${total}\n";
                    lineItems += lineItem;
                    subtotal += total;
                }
                ICaclulator vatCaclulator;
                vatCaclulator = vatType == 1
                ? new StandardVatCalculator()
                : new ZeroRatedVatCalculator();
                ICaclulator discountCalculator = new DiscountCalculator(discountPercent);
                decimal DiscountAmount = discountCalculator.Calculate(subtotal);
                decimal TaxableAmount = subtotal - DiscountAmount;
                Console.WriteLine($@"
            --- Invoice Summary ---
            Customer:      {customer?.Name}  
            Line items:
            ${lineItems}
            Subtotal:                            {subtotal}$
            Discount (${discountPercent}%):       -${DiscountAmount}
            Taxable amount:                      ${TaxableAmount}
            Tax (VAT):                       +${vatCaclulator.Calculate(TaxableAmount)}
           -----------------------------------------------
           Total:                                $139.73
           Due date:                             2026-09-27
           Status:                               Draft
           ");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
    }
}