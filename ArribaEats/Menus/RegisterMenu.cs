using System;
using ArribaEats.Models;
using ArribaEats.Repositories;
using ArribaEats.Services;

namespace ArribaEats.Menus
{
    /// <summary>
    /// Provides the registration menu for new users.
    /// </summary>
    public static class RegisterMenu
    {
        private static RestaurantRepository _restaurantRepo = RestaurantRepository.Instance;

        /// <summary>
        /// Runs the registration menu, allowing the user to select a user type to register as.
        /// </summary>
        public static void Run()
        {
            // Main menu loop for registration
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

        /// <summary>
        /// Prompts the user for a valid name.
        /// </summary>
        /// <returns>The validated name.</returns>
        private static string PromptValidName()
        {
            // Prompt for a valid name
            while (true)
            {
                Console.WriteLine("Please enter your name: ");
                string name = Console.ReadLine();
                if (ValidationService.IsValidName(name))
                    return name;
                Console.WriteLine("Invalid name.");
            }
        }

        /// <summary>
        /// Prompts the user for a valid age.
        /// </summary>
        /// <returns>The validated age.</returns>
        private static int PromptValidAge()
        {
            // Prompt for a valid age
            while (true)
            {
                Console.WriteLine("Please enter your age (18-100): ");
                string input = Console.ReadLine();
                if (ValidationService.IsValidAge(input, out int age))
                    return age;
                Console.WriteLine("Invalid age.");
            }
        }

        /// <summary>
        /// Prompts the user for a valid email address.
        /// </summary>
        /// <returns>The validated email address.</returns>
        private static string PromptValidEmail()
        {
            // Prompt for a valid email address
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

        /// <summary>
        /// Prompts the user for a valid phone number.
        /// </summary>
        /// <returns>The validated phone number.</returns>
        private static string PromptValidPhone()
        {
            // Prompt for a valid phone number
            while (true)
            {
                Console.WriteLine("Please enter your mobile phone number: ");
                string phone = Console.ReadLine();
                if (ValidationService.IsValidPhone(phone))
                    return phone;
                Console.WriteLine("Invalid phone number.");
            }
        }

        /// <summary>
        /// Prompts the user for a valid password and confirmation.
        /// </summary>
        /// <returns>The validated password.</returns>
        private static string PromptValidPassword()
        {
            // Prompt for a valid password and confirmation
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

        /// <summary>
        /// Handles the registration process for a customer.
        /// </summary>
        private static void RegisterCustomer()
        {
            // Register a new customer
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

        /// <summary>
        /// Handles the registration process for a deliverer.
        /// </summary>
        private static void RegisterDeliverer()
        {
            // Register a new deliverer
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

        /// <summary>
        /// Handles the registration process for a client (restaurant owner).
        /// </summary>
        private static void RegisterClient()
        {
            // Register a new client (restaurant owner)
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

            var restaurant = new Restaurant(restaurantName, style, location.x, location.y, email);
            var client = new Client(name, age, email, phone, password, restaurant);
            _restaurantRepo.AddRestaurant(restaurant);
            UserService.AddUser(client);

            Console.WriteLine($"You have been successfully registered as a client, {name}!");
        }
    }
}