using ArribaEats.Helpers;
using ArribaEats.Models;
using ArribaEats.Repositories;
using ArribaEats.Services;

public static class ClientMainMenu
{
    private static RestaurantRepository _restaurantRepo = RestaurantRepository.Instance;
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

                case "7":
                    Console.WriteLine("You are now logged out.");
                    return;

                default:
                    Console.WriteLine("Feature not implemented yet.");
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
        foreach (var item in restaurant.MenuItems)
        {
            Console.WriteLine($"  ${item.Price:F2}  {item.Name}");
        }

        while (true)
        {
            Console.WriteLine("Please enter the name of the new item (blank to cancel):");
            var itemName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(itemName))
                return;

            decimal price;
            int attempts = 0;
            while (true)
            {
                if (attempts > 0)
                    Console.WriteLine("Invalid price.");

                Console.WriteLine("Please enter the price of the new item (without the $):");
                var priceInput = Console.ReadLine();

                if (ValidationService.IsValidItemPrice(priceInput, out price))
                    break;

                attempts++;
            }

            restaurant.MenuItems.Add(new MenuItem(itemName, price));
            Console.WriteLine($"Successfully added {itemName} (${price:F2}) to menu.");
            return;
        }
    }
}