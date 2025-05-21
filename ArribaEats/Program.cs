using ArribaEats.Menus;
using ArribaEats.Services;

namespace ArribaEats
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Arriba Eats!");

            while (true)
            {
                Console.WriteLine("\nPlease make a choice from the menu below:");
                Console.WriteLine("1: Login as a registered user");
                Console.WriteLine("2: Register as a new user");
                Console.WriteLine("3: Exit");
                Console.Write("Please enter a choice between 1 and 3: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // if (LoginMenu.Show())
                        // {
                        //     // Login successful, show respective main menu (not implemented yet)
                        // }
                        break;

                    case "2":
                        RegisterMenu.Run();
                        break;

                    case "3":
                        Console.WriteLine("Thank you for using Arriba Eats!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}