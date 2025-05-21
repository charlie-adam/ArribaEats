namespace ArribaEats.Models
{
    public class Order
    {
        public int OrderId { get; }
        public Client Client { get; }
        public Restaurant Restaurant { get; set; }

        public Customer Customer { get; }
        public Deliverer Deliverer { get; private set; }  // Assigned deliverer, can be null initially
        public string Items { get; }  // Could be expanded to a list of item objects later
        public string Status { get; private set; }  // e.g., "Pending", "In Transit", "Delivered"

        public Order(int orderId, Client client, Customer customer, string items)
        {
            OrderId = orderId;
            Client = client;
            Customer = customer;
            Items = items;
            Status = "Pending";
        }

        public void AssignDeliverer(Deliverer deliverer)
        {
            Deliverer = deliverer;
            Status = "In Transit";
        }

        public void MarkDelivered()
        {
            Status = "Delivered";
        }
    }
}