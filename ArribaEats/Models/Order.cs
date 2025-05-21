namespace ArribaEats.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Customer Customer { get; set; }
        public Deliverer Deliverer { get; set; }
        public bool IsRated { get; set; } = false;
        public string Status { get; set; } = "Pending"; // Example statuses: Pending, In Transit, Delivered

        public Order(int orderId, Restaurant restaurant, Customer customer)
        {
            OrderId = orderId;
            Restaurant = restaurant;
            Customer = customer;
        }
    }
}