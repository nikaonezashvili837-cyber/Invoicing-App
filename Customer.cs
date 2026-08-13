namespace InvoicingApp
{
    class Customer
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Customer(string? id , string? name, string? email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        public void Deconstruct(out string? id , out string? name , out string? email)
        {
            id = Id;
            name = Name;
            email = Email;
        }
    }
}