using System;
using ArribaEats.Models;
using ArribaEats.Services;

namespace ArribaEats.Menus
{
    public static class RegisterMenu
    {
        public static void Run()
        {
            while (true)
            {
                Console.WriteLine("Which type of user would you like to register as?");
                Console.WriteLine("1: Customer");
                Console.WriteLine("2: Deliverer");
                Console.WriteLine("3: Client");
                Console.WriteLine("4: Return to the previous menu");
                Console.WriteLine("Please enter a choice between 1 and 4: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterCustomer();
                        return;
                    case "2":
                        RegisterDeliverer();
                        return;
                    case "3":
                        RegisterClient();
                        return;
                    case "4":
                        return; // Return to previous menu
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private static string PromptValidName()
        {
            while (true)
            {
                Console.WriteLine("Please enter your name: ");
                string name = Console.ReadLine();
                if (ValidationService.IsValidName(name))
                    return name;
                Console.WriteLine("Invalid name.");
            }
        }

        private static int PromptValidAge()
        {
            while (true)
            {
                Console.WriteLine("Please enter your age (18-100): ");
                string input = Console.ReadLine();
                if (ValidationService.IsValidAge(input, out int age))
                    return age;
                Console.WriteLine("Invalid age.");
            }
        }

        private static string PromptValidEmail()
        {
            while (true)
            {
                Console.WriteLine("Please enter your email address: ");
                string email = Console.ReadLine();
                if (!ValidationService.IsValidEmail(email))
                {
                    Console.WriteLine("Invalid email address.");
                    continue;
                }

                if (UserService.EmailExists(email))
                {
                    Console.WriteLine("This email address is already in use.");
                    continue;
                }
                return email;
            }
        }

        private static string PromptValidPhone()
        {
            while (true)
            {
                Console.WriteLine("Please enter your mobile phone number: ");
                string phone = Console.ReadLine();
                if (ValidationService.IsValidPhone(phone))
                    return phone;
                Console.WriteLine("Invalid phone number.");
            }
        }

        private static string PromptValidPassword()
        {
            while (true)
            {
                Console.WriteLine("Your password must:");
                Console.WriteLine("- be at least 8 characters long");
                Console.WriteLine("- contain a number");
                Console.WriteLine("- contain a lowercase letter");
                Console.WriteLine("- contain an uppercase letter");
                Console.WriteLine("Please enter a password: ");
                string password = Console.ReadLine();

                if (!ValidationService.IsValidPassword(password))
                {
                    Console.WriteLine("Invalid password.");
                    continue;
                }

                Console.WriteLine("Please confirm your password: ");
                string confirmPassword = Console.ReadLine();

                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match.");
                    continue;
                }
                return password;
            }
        }

        private static void RegisterCustomer()
        {
            string name = PromptValidName();
            int age = PromptValidAge();
            string email = PromptValidEmail();
            string phone = PromptValidPhone();
            string password = PromptValidPassword();

            (int x, int y) location;
            while (true)
            {
                Console.WriteLine("Please enter your location (in the form of X,Y): ");
                string input = Console.ReadLine();
                if (ValidationService.IsValidLocation(input, out location))
                    break;
                Console.WriteLine("Invalid location.");
            }

            var customer = new Customer(name, age, email, phone, password, location);
            UserService.AddUser(customer);

            Console.WriteLine($"You have been successfully registered as a customer, {name}!");
        }

        private static void RegisterDeliverer()
        {
            string name = PromptValidName();
            int age = PromptValidAge();
            string email = PromptValidEmail();
            string phone = PromptValidPhone();
            string password = PromptValidPassword();

            string licencePlate;
            while (true)
            {
                Console.WriteLine("Please enter your licence plate: ");
                licencePlate = Console.ReadLine();
                if (ValidationService.IsValidLicencePlate(licencePlate))
                    break;
                Console.WriteLine("Invalid licence plate.");
            }

            var deliverer = new Deliverer(name, age, email, phone, password, licencePlate);
            UserService.AddUser(deliverer);

            Console.WriteLine($"You have been successfully registered as a deliverer, {name}!");
        }

        private static void RegisterClient()
        {
            string name = PromptValidName();
            int age = PromptValidAge();
            string email = PromptValidEmail();
            string phone = PromptValidPhone();
            string password = PromptValidPassword();

            string restaurantName;
            while (true)
            {
                Console.WriteLine("Please enter your restaurant's name: ");
                restaurantName = Console.ReadLine();
                if (ValidationService.IsValidRestaurantName(restaurantName))
                    break;
                Console.WriteLine("Invalid restaurant name.");
            }

            string style;
            while (true)
            {
                Console.WriteLine("Please select your restaurant's style:");
                Console.WriteLine("1: Italian");
                Console.WriteLine("2: French");
                Console.WriteLine("3: Chinese");
                Console.WriteLine("4: Japanese");
                Console.WriteLine("5: American");
                Console.WriteLine("6: Australian");
                Console.WriteLine("Please enter a choice between 1 and 6: ");
                string choice = Console.ReadLine();

                if (ValidationService.IsValidFoodStyle(choice, out int styleNum))
                {
                    style = choice switch
                    {
                        "1" => "Italian",
                        "2" => "French",
                        "3" => "Chinese",
                        "4" => "Japanese",
                        "5" => "American",
                        "6" => "Australian",
                        _ => null
                    };
                    if (style != null) break;
                }
                Console.WriteLine("Invalid choice.");
            }

            (int x, int y) location;
            while (true)
            {
                Console.WriteLine("Please enter your location (in the form of X,Y): ");
                string input = Console.ReadLine();
                if (ValidationService.IsValidLocation(input, out location))
                    break;
                Console.WriteLine("Invalid location.");
            }
            
            var restaraunt = new Restaurant(restaurantName, style, location.x, location.y);

            var client = new Client(name, age, email, phone, password, new Restaurant(restaurantName, style, location.x, location.y));
            UserService.AddUser(client);

            Console.WriteLine($"You have been successfully registered as a client, {name}!");
        }
    }
}