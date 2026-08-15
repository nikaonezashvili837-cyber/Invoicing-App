namespace InvoicingApp
{
    class Product
    {
        public string? Id {get;set;}
        public string? Name {get;set;}
        public string? Category {get;set;}
        public decimal Price {get;set;}
        public Product(string Id , string Name , string Category , decimal Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Category = Category;
            this.Price = Price;
        }
    }
}