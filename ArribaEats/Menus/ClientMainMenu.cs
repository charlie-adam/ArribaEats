using System;
using ArribaEats.Models;
using ArribaEats.Helpers;

namespace ArribaEats.Menus
{
    public static class ClientMainMenu
    {
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

                    case "7":
                        Console.WriteLine("You are now logged out.");
                        return;

                    default:
                        Console.WriteLine("Feature not implemented yet.");
                        break;
                }
            }
        }
    }
}