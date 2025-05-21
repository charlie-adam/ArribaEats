using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;

namespace ArribaEats.Repositories
{
    public class OrderRepository
    {
        private readonly List<Order> _orders = new();

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

        public List<Order> GetOrdersByDeliverer(Deliverer deliverer)
        {
            return _orders.Where(o => o.Deliverer == deliverer).ToList();
        }
    }
}