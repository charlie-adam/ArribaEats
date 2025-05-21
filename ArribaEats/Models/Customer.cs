namespace ArribaEats.Models
{
    public class Customer : User
    {
        public (int X, int Y) Location { get; }

        public Customer(string name, int age, string email, string mobile, string password, (int, int) location)
            : base(name, age, email, mobile, password)
        {
            Location = location;
        }

        public override string Role => "Customer";
    }
}