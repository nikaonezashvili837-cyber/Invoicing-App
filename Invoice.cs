using System.Security.Cryptography;

namespace InvoicingApp
{
    struct SelectedProductData
    {
        public Product SelectedProduct { get; }
        public int Amount { get; }
        public SelectedProductData(Product SelectedProduct, int Amount)
        {
            this.SelectedProduct = SelectedProduct;
            this.Amount = Amount;
        }
    }
    class Invoice
    {


        public string? Customer { get; set; }
        public List<SelectedProductData>? SelectedProducts { get; set; }
    }
}