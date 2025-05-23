using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;

namespace ArribaEats.Repositories
{
    /// <summary>
    /// Repository for managing order data in memory.
    /// </summary>
    public sealed class OrderRepository
    {
        private static readonly OrderRepository _instance = new OrderRepository();

        /// <summary>
        /// Gets the singleton instance of the OrderRepository.
        /// </summary>
        public static OrderRepository Instance => _instance;

        private readonly List<Order> _orders = new();
        private int _nextOrderId = 1;

        // Private constructor to prevent external instantiation
        private OrderRepository() { }

        /// <summary>
        /// Generates a unique order ID.
        /// </summary>
        /// <returns>The next available order ID.</returns>
        public int GetNextOrderId()
        {
            // Increment and return the next order ID
            return _nextOrderId++;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public List<Order> GetOrders()
        {
            // Return all orders
            return _orders;
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="updatedOrder">The updated order object.</param>
        public void UpdateOrder(Order updatedOrder)
        {
            // Find and update the order in the list
            var index = _orders.FindIndex(o => o.OrderId == updatedOrder.OrderId);
            if (index != -1)
            {
                _orders[index] = updatedOrder;
            }
        }
        
        /// <summary>
        /// Gets all orders for a specific restaurant by name.
        /// </summary>
        /// <param name="restaurantName">The name of the restaurant.</param>
        /// <returns>A list of orders for the restaurant.</returns>
        public List<Order> GetOrdersByRestaurantName(string restaurantName)
        {
            // Filter orders by restaurant name
            return _orders
                .Where(o => o.Restaurant.Name == restaurantName)
                .ToList();
        }

        /// <summary>
        /// Adds a new order to the repository.
        /// </summary>
        /// <param name="order">The order to add.</param>
        public void AddOrder(Order order)
        {
            // Add order to the list
            _orders.Add(order);
        }

        /// <summary>
        /// Gets an order by its ID.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public Order GetOrderById(int orderId)
        {
            // Find order by ID
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        /// <summary>
        /// Gets all orders for a specific customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>A list of orders for the customer.</returns>
        public List<Order> GetOrdersByCustomer(Customer customer)
        {
            // Filter orders by customer object
            return _orders.Where(o => o.Customer == customer).ToList();
        }

        /// <summary>
        /// Gets all orders for a customer by email.
        /// </summary>
        /// <param name="email">The customer's email.</param>
        /// <returns>A list of orders for the customer.</returns>
        public List<Order> GetOrdersByCustomerEmail(string email)
        {
            // Filter orders by customer email
            return _orders.Where(o => o.Customer.Email == email).ToList();
        }

        /// <summary>
        /// Gets all orders assigned to a specific deliverer.
        /// </summary>
        /// <param name="deliverer">The deliverer.</param>
        /// <returns>A list of orders for the deliverer.</returns>
        public List<Order> GetOrdersByDeliverer(Deliverer deliverer)
        {
            // Filter orders by deliverer object
            return _orders.Where(o => o.Deliverer == deliverer).ToList();
        }

        /// <summary>
        /// Marks an order as rated.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        public void MarkOrderAsRated(int orderId)
        {
            // Set IsRated to true for the specified order
            var order = GetOrderById(orderId);
            if (order != null)
            {
                order.IsRated = true;
            }
        }
    }
}