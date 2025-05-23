using System;
using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;
using ArribaEats.Helpers;
using ArribaEats.Repositories;

namespace ArribaEats.Menus
{
    public static class DelivererMainMenu
    {
        public static void Show(Deliverer deliverer)
        {
            var orderRepo = OrderRepository.Instance;

            while (true)
            {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: List orders available to deliver");
                Console.WriteLine("3: Arrived at restaurant to pick up order");
                Console.WriteLine("4: Mark this delivery as complete");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UserInformationDisplay.DisplayDelivererInfo(deliverer);
                        break;

                    case "2":
                        if (deliverer.CurrentDelivery != null)
                        {
                            Console.WriteLine("You have already selected an order for delivery.");
                            break;
                        }

                        (int x, int y)? location = GetLocationFromInput();
                        if (location == null)
                            break;

                        var availableOrders = orderRepo.GetOrders()
                            .Where(o => (o.Deliverer == null)).OrderBy(o => o.OrderId).ToList();

                        if (availableOrders.Count == 0)
                        {
                            Console.WriteLine("No orders currently available for delivery.");
                            break;
                        }

                        Console.WriteLine("\nThe following orders are available for delivery. Select an order to accept it:");
                        Console.WriteLine("   Order  Restaurant Name       Loc    Customer Name    Loc    Dist");

                        for (int i = 0; i < availableOrders.Count; i++)
                        {
                            var order = availableOrders[i];
                            int dist = GetDistance(location.Value.x, location.Value.y, order.Restaurant.LocationX, order.Restaurant.LocationY)
                                     + GetDistance(order.Restaurant.LocationX, order.Restaurant.LocationY, order.Customer.Location.X, order.Customer.Location.Y);

                            Console.WriteLine($"{i + 1}: {order.OrderId,6}  {order.Restaurant.Name,-18}  {order.Restaurant.LocationX},{order.Restaurant.LocationY}  {order.Customer.Name,-16}  {order.Customer.Location.X},{order.Customer.Location.Y}  {dist}");
                        }

                        Console.WriteLine($"{availableOrders.Count + 1}: Return to the previous menu");
                        Console.WriteLine($"Please enter a choice between 1 and {availableOrders.Count + 1}: ");
                        if (!int.TryParse(Console.ReadLine(), out int selected) || selected < 1 || selected > availableOrders.Count + 1)
                        {
                            Console.WriteLine("Invalid choice.");
                            break;
                        }

                        if (selected == availableOrders.Count + 1)
                            break;

                        var selectedOrder = availableOrders[selected - 1];
                        deliverer.CurrentDelivery = selectedOrder;
                        selectedOrder.Deliverer = deliverer;
                        selectedOrder.Status = Order.OrderStatus.Ordered;

                        Console.WriteLine($"Thanks for accepting the order. Please head to {selectedOrder.Restaurant.Name} at {selectedOrder.Restaurant.LocationX},{selectedOrder.Restaurant.LocationY} to pick it up.");
                        break;

                    case "3":
                        if (deliverer.CurrentDelivery == null)
                        {
                            Console.WriteLine("You have not yet accepted an order.");
                            break;
                        }

                        if (deliverer.CurrentDelivery.Status == Order.OrderStatus.Delivered)
                        {
                            Console.WriteLine("You have already picked up this order.");
                            break;
                        }

                        deliverer.HasArrived = true;

                        Console.WriteLine($"Thanks. We have informed {deliverer.CurrentDelivery.Restaurant.Name} that you have arrived and are ready to pick up order #{deliverer.CurrentDelivery.OrderId}.");
                        Console.WriteLine("Please show the staff this screen as confirmation.");
                        Console.WriteLine($"When you have the order, please deliver it to {deliverer.CurrentDelivery.Customer.Name} at {deliverer.CurrentDelivery.Customer.Location.X},{deliverer.CurrentDelivery.Customer.Location.Y}.");
                        break;

                    case "4":
                        if (deliverer.CurrentDelivery == null)
                        {
                            Console.WriteLine("You have not yet accepted an order.");
                            break;
                        }

                        if (deliverer.CurrentDelivery.Status != Order.OrderStatus.BeingDelivered)
                        {
                            Console.WriteLine("You have not yet picked up this order.");
                            break;
                        }

                        Console.WriteLine("Thank you for making the delivery.");
                        deliverer.CurrentDelivery.Status = Order.OrderStatus.Delivered;
                        deliverer.CurrentDelivery = null;
                        break;

                    case "5":
                        Console.WriteLine("You are now logged out.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private static (int x, int y)? GetLocationFromInput()
        {
            while (true)
            {
                Console.WriteLine("Please enter your location (in the form of X,Y): ");
                var input = Console.ReadLine();
                var parts = input?.Split(',');

                if (parts != null && parts.Length == 2 &&
                    int.TryParse(parts[0].Trim(), out int x) &&
                    int.TryParse(parts[1].Trim(), out int y))
                {
                    return (x, y);
                }

                Console.WriteLine("Invalid location.");
            }
        }

        private static int GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2); // Taxicab distance
        }
    }
}