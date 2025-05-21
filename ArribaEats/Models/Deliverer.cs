namespace ArribaEats.Models
{
    public class Deliverer : User
    {
        public string LicencePlate { get; }

        public Deliverer(string name, int age, string email, string mobile, string password, string licencePlate)
            : base(name, age, email, mobile, password)
        {
            LicencePlate = licencePlate;
        }

        public override string Role => "Deliverer";
    }
}