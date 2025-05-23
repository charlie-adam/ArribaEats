namespace ArribaEats.Models
{
    public class Deliverer : User
    {
        public string LicencePlate { get; }
        public Order? CurrentDelivery { get; set; }
        public bool HasArrived { get; set; } = false;

        public Deliverer(string name, int age, string email, string mobile, string password, string licencePlate, Order? currentDelivery = null)
            : base(name, age, email, mobile, password)
        {
            LicencePlate = licencePlate;
            CurrentDelivery = currentDelivery;
        }

        public override string Role => "Deliverer";
    }
}