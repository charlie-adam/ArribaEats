using System.Collections.Generic;

namespace ArribaEats.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Customer Customer { get; set; }
        public Deliverer Deliverer { get; set; }
        public bool IsRated { get; set; } = false;
        public OrderStatus Status { get; set; } = OrderStatus.Ordered;
        public Dictionary<MenuItem, int> OrderItems { get; set; } = new();
        public Order(int orderId, Restaurant restaurant, Customer customer)
        {
            OrderId = orderId;
            Restaurant = restaurant;
            Customer = customer;
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (var item in OrderItems)
            {
                total += item.Key.Price * item.Value;  // Price from MenuItem, quantity is int value
            }
            return total;
        }

        //orderStatus enum
        public enum OrderStatus
        {
            Ordered,
            Delivered,
            Cooking,
            BeingDelivered,
            Cooked
        }
    }
}