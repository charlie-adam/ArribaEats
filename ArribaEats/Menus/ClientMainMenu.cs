using ArribaEats.Helpers;
using ArribaEats.Models;
using ArribaEats.Repositories;
using ArribaEats.Services;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ClientMainMenu
{
    private static RestaurantRepository _restaurantRepo = RestaurantRepository.Instance;
    private static OrderRepository _orderRepo = OrderRepository.Instance;

    public static void Show(Client client)
    {
        while (true)
        {
            Console.WriteLine("Please make a choice from the menu below:");
            Console.WriteLine("1: Display your user information");
            Console.WriteLine("2: Add item to restaurant menu");
            Console.WriteLine("3: See current orders");
            Console.WriteLine("4: Start cooking order");
            Console.WriteLine("5: Finish cooking order");
            Console.WriteLine("6: Handle deliverers who have arrived");
            Console.WriteLine("7: Log out");
            Console.WriteLine("Please enter a choice between 1 and 7: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UserInformationDisplay.DisplayClientInfo(client);
                    break;
                case "2":
                    AddMenuItem(client);
                    break;
                case "3":
                    ShowCurrentOrders(client);
                    break;
                case "4":
                    StartCookingOrder(client);
                    break;
                case "5":
                    FinishCookingOrder(client);
                    break;
                case "6":
                    HandleDeliverers(client);
                    break;
                case "7":
                    Console.WriteLine("You are now logged out.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    private static void AddMenuItem(Client client)
    {
        var restaurant = _restaurantRepo.GetByOwnerEmail(client.Email);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        Console.WriteLine("This is your restaurant's current menu:");
        foreach (var item in restaurant.Menu)
        {
            Console.WriteLine($"{item.Price,7:C2}  {item.Name}");
        }

        while (true)
        {
            Console.WriteLine("Please enter the name of the new item (blank to cancel): ");
            var itemName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(itemName))
                return;

            decimal price;
            int attempts = 0;
            while (true)
            {
                if (attempts > 0)
                    Console.WriteLine("Invalid price.");

                Console.WriteLine("Please enter the price of the new item (without the $): ");
                var priceInput = Console.ReadLine();

                if (ValidationService.IsValidItemPrice(priceInput, out price))
                    break;

                attempts++;
            }

            restaurant.Menu.Add(new MenuItem(itemName, price));
            Console.WriteLine($"Successfully added {itemName} (${price:F2}) to menu.");
            return;
        }
    }

    private static void ShowCurrentOrders(Client client)
    {
        var restaurant = _restaurantRepo.GetByOwnerEmail(client.Email);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        var orders = _orderRepo.GetOrdersByRestaurantName(restaurant.Name)
                               .Where(o => o.Status != Order.OrderStatus.Delivered)
                               .ToList();

        if (orders.Count == 0)
        {
            Console.WriteLine("Your restaurant has no current orders.");
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine($"\nOrder #{order.OrderId} for {order.Customer.Name}: {order.Status}");
            PrintItemTotals(order);
        }
    }

    private static void StartCookingOrder(Client client)
    {
        var restaurant = _restaurantRepo.GetByOwnerEmail(client.Email);
        var orders = _orderRepo.GetOrdersByRestaurantName(restaurant.Name)
                               .Where(o => o.Status == Order.OrderStatus.Ordered)
                               .ToList();

        if (orders.Count == 0)
        {
            Console.WriteLine("There are no orders to start cooking.");
            return;
        }

        Console.WriteLine("Select an order once you are ready to start cooking:");
        for (int i = 0; i < orders.Count; i++)
        {
            Console.WriteLine($"{i + 1}: Order #{orders[i].OrderId} for {orders[i].Customer.Name}");
        }
        Console.WriteLine($"{orders.Count + 1}: Return to the previous menu");
        Console.WriteLine("Please enter a choice between 1 and " + (orders.Count + 1) + ": ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > orders.Count + 1)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        if (choice == orders.Count + 1)
            return;

        var selectedOrder = orders[choice - 1];
        selectedOrder.Status = Order.OrderStatus.Cooking;
        _orderRepo.UpdateOrder(selectedOrder);
        Console.WriteLine($"Order #{selectedOrder.OrderId} is now marked as cooking. Please prepare the order, then mark it as finished cooking:");
        PrintItemTotals(selectedOrder);
    }

    private static void FinishCookingOrder(Client client)
    {
        var restaurant = _restaurantRepo.GetByOwnerEmail(client.Email);
        var orders = _orderRepo.GetOrdersByRestaurantName(restaurant.Name)
                               .Where(o => o.Status == Order.OrderStatus.Cooking)
                               .ToList();

        if (orders.Count == 0)
        {
            Console.WriteLine("There are no orders that are currently cooking.");
            return;
        }

        Console.WriteLine("Select an order once you have finished preparing it:");
        for (int i = 0; i < orders.Count; i++)
        {
            Console.WriteLine($"{i + 1}: Order #{orders[i].OrderId} for {orders[i].Customer.Name}");
        }
        Console.WriteLine($"{orders.Count + 1}: Return to the previous menu");
        Console.WriteLine("Please enter a choice between 1 and " + (orders.Count + 1) + ": ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > orders.Count + 1)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        if (choice == orders.Count + 1)
            return;

        var selectedOrder = orders[choice - 1];
        selectedOrder.Status = Order.OrderStatus.Cooked;
        _orderRepo.UpdateOrder(selectedOrder);
        Console.WriteLine($"Order #{selectedOrder.OrderId} is now ready for collection.");

        if (selectedOrder.Deliverer == null)
        {
            Console.WriteLine("No deliverer has been assigned yet.");
        }
        else if (selectedOrder.Deliverer.HasArrived)
        {
            Console.WriteLine($"Please take it to the deliverer with licence plate {selectedOrder.Deliverer.LicencePlate}, who is waiting to collect it.");
        }
        else
        {
            Console.WriteLine($"The deliverer with licence plate {selectedOrder.Deliverer.LicencePlate} will be arriving soon to collect it.");
        }
    }

    private static void HandleDeliverers(Client client)
    {
        var restaurant = _restaurantRepo.GetByOwnerEmail(client.Email);
        var orders = _orderRepo.GetOrdersByRestaurantName(restaurant.Name)
                               .Where(o => o.Deliverer?.HasArrived == true && o.Status != Order.OrderStatus.BeingDelivered)
                               .ToList();

        if (orders.Count == 0)
        {
            Console.WriteLine("No deliverers are currently waiting to collect orders.");
            return;
        }


        Console.WriteLine("These deliverers have arrived and are waiting to collect orders.");
        Console.WriteLine("Select an order to indicate that the deliverer has collected it:");
        for (int i = 0; i < orders.Count; i++)
        {
            Console.WriteLine($"{i + 1}: Order #{orders[i].OrderId} for {orders[i].Customer.Name} (Deliverer licence plate: {orders[i].Deliverer.LicencePlate}) (Order status: {orders[i].Status})");
        }
        Console.WriteLine($"{orders.Count + 1}: Return to the previous menu");
        Console.WriteLine("Please enter a choice between 1 and " + (orders.Count + 1) + ": ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > orders.Count + 1)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        if (choice == orders.Count + 1)
            return;

        var selectedOrder = orders[choice - 1];
        selectedOrder.Status = Order.OrderStatus.BeingDelivered;
        _orderRepo.UpdateOrder(selectedOrder);
        Console.WriteLine($"Order #{selectedOrder.OrderId} is now marked as being delivered.");
    }

    private static void PrintItemTotals(Order order)
    {
        var itemTotals = new Dictionary<string, int>();
        foreach (var item in order.OrderItems)
        {
            if (itemTotals.ContainsKey(item.Key.Name))
                itemTotals[item.Key.Name] += item.Value;
            else
                itemTotals[item.Key.Name] = item.Value;
        }
        foreach (var item in itemTotals)
        {
            Console.WriteLine($"{item.Value} x {item.Key}");
        }
    }
}