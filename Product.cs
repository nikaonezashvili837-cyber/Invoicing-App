namespace InvoicingApp
{
    class Product
    {
        public string? Id {get;set;}
        public string? ProductName {get;set;}
        public string? Category {get;set;}
        public decimal Price {get;set;}
        public Product(string Id , string Name ,  decimal Price)
        {
            this.Id = Id;
            this.ProductName = Name;
            this.Price = Price;
        }
        public void Deconstruct(out string? id , out string? productName,  out decimal? price)
        {
            id = Id;
            productName = ProductName;
            price = Price;
        }
    }
}