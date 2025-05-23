using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;

namespace ArribaEats.Repositories
{
    public sealed class OrderRepository
    {
        private static readonly OrderRepository _instance = new OrderRepository();
        public static OrderRepository Instance => _instance;

        private readonly List<Order> _orders = new();
        private int _nextOrderId = 1;

        // Private constructor to prevent external instantiation
        private OrderRepository() { }

        // Generate a unique order ID
        public int GetNextOrderId()
        {
            return _nextOrderId++;
        }

        public List<Order> GetOrders()
        {
            return _orders;
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var index = _orders.FindIndex(o => o.OrderId == updatedOrder.OrderId);
            if (index != -1)
            {
                _orders[index] = updatedOrder;
            }
        }
        
        public List<Order> GetOrdersByRestaurantName(string restaurantName)
        {
            return _orders
                .Where(o => o.Restaurant.Name == restaurantName)
                .ToList();
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetOrdersByCustomer(Customer customer)
        {
            return _orders.Where(o => o.Customer == customer).ToList();
        }

        public List<Order> GetOrdersByCustomerEmail(string email)
        {
            return _orders.Where(o => o.Customer.Email == email).ToList();
        }

        public List<Order> GetOrdersByDeliverer(Deliverer deliverer)
        {
            return _orders.Where(o => o.Deliverer == deliverer).ToList();
        }

        public void MarkOrderAsRated(int orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {
                order.IsRated = true;
            }
        }
    }
}