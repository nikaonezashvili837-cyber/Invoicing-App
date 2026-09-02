using System.Security.Cryptography;

namespace InvoicingApp
{
    struct SelectedProductData
    {
        public string? ProductName {get;}
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
        private List<SelectedProductData> SelectedProducts = new List<SelectedProductData>();
        public void AddProduct(SelectedProductData selectedProduct)
        {
            SelectedProducts.Add(selectedProduct);
        }
        public List<SelectedProductData> RetriveProducts()
        {
            return SelectedProducts;
        }
    }
}