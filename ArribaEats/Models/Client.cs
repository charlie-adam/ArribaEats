namespace ArribaEats.Models
{
    public class Client : User
    {
        public Restaurant Restaurant { get; }
        public Client(string name, int age, string email, string mobile, string password, Restaurant restaurant)
            : base(name, age, email, mobile, password)
        {
            Restaurant = restaurant;
        }

        public override string Role => "Client";
    }
}