namespace ArribaEats.Models
{
    public class Client : User
    {
        public string RestaurantName { get; }
        public string Style { get; }
        public (int X, int Y) Location { get; }

        public Client(string name, int age, string email, string mobile, string password, string restaurantName, string style, (int, int) location)
            : base(name, age, email, mobile, password)
        {
            RestaurantName = restaurantName;
            Style = style;
            Location = location;
        }

        public override string Role => "Client";
    }
}