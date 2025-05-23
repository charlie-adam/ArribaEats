using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArribaEats.Services
{
    /// <summary>
    /// Provides static methods for validating user and restaurant input data.
    /// </summary>
    public static class ValidationService
    {
        /// <summary>
        /// Validates a name to ensure it contains only letters, spaces, apostrophes, and hyphens, and at least one letter.
        /// </summary>
        /// <param name="name">The name string to validate.</param>
        /// <returns>True if the name is valid; otherwise, false.</returns>
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            // Check that all chars are allowed
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s'\-]+$")) return false;
            // Check at least one letter
            return name.Any(char.IsLetter);
        }

        /// <summary>
        /// Validates an age input string to ensure it is an integer between 18 and 100 inclusive.
        /// </summary>
        /// <param name="input">The age input string.</param>
        /// <param name="age">The parsed age if valid.</param>
        /// <returns>True if the age is valid; otherwise, false.</returns>
        public static bool IsValidAge(string input, out int age) =>
            int.TryParse(input, out age) && age is >= 18 and <= 100;

        /// <summary>
        /// Validates an email address to ensure it contains exactly one '@' and at least one character before and after.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True if the email is valid; otherwise, false.</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var parts = email.Split('@');
            if (parts.Length != 2) return false;
            if (string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1])) return false;
            return true;
        }

        /// <summary>
        /// Validates a phone number to ensure it is exactly 10 digits and starts with '0'.
        /// </summary>
        /// <param name="phone">The phone number string to validate.</param>
        /// <returns>True if the phone number is valid; otherwise, false.</returns>
        public static bool IsValidPhone(string phone) =>
            phone != null &&
            phone.Length == 10 &&
            phone.All(char.IsDigit) &&
            phone.StartsWith("0");

        /// <summary>
        /// Validates a password to ensure it is at least 8 characters long and contains lowercase, uppercase, and digit characters.
        /// </summary>
        /// <param name="password">The password string to validate.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            return password.Length >= 8 &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit);
        }

        /// <summary>
        /// Validates a location input string in the format "X,Y" where both X and Y are integers.
        /// </summary>
        /// <param name="input">The location input string.</param>
        /// <param name="location">The parsed location tuple if valid.</param>
        /// <returns>True if the location is valid; otherwise, false.</returns>
        public static bool IsValidLocation(string input, out (int, int) location)
        {
            location = (0, 0);
            if (string.IsNullOrWhiteSpace(input)) return false;
            var parts = input.Split(',');
            if (parts.Length != 2) return false;
            if (!int.TryParse(parts[0].Trim(), out int x)) return false;
            if (!int.TryParse(parts[1].Trim(), out int y)) return false;
            location = (x, y);
            return true;
        }

        /// <summary>
        /// Validates a licence plate to ensure it is 1-8 characters, contains only uppercase letters, digits, and spaces, and is not all spaces.
        /// </summary>
        /// <param name="plate">The licence plate string to validate.</param>
        /// <returns>True if the licence plate is valid; otherwise, false.</returns>
        public static bool IsValidLicencePlate(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate)) return false;
            if (plate.Length < 1 || plate.Length > 8) return false;
            // Only uppercase letters, digits and spaces allowed
            if (!Regex.IsMatch(plate, @"^[A-Z0-9 ]+$")) return false;
            // Must not be all spaces
            if (plate.All(char.IsWhiteSpace)) return false;
            return true;
        }

        /// <summary>
        /// Validates a food style input to ensure it is a numeric value between 1 and 6 inclusive.
        /// </summary>
        /// <param name="input">The food style input string.</param>
        /// <param name="style">The parsed style number if valid.</param>
        /// <returns>True if the food style is valid; otherwise, false.</returns>
        public static bool IsValidFoodStyle(string input, out int style)
        {
            if (int.TryParse(input, out style))
            {
                return style >= 1 && style <= 6;
            }
            return false;
        }

        /// <summary>
        /// Validates a restaurant name to ensure it contains at least one non-whitespace character.
        /// </summary>
        /// <param name="name">The restaurant name to validate.</param>
        /// <returns>True if the restaurant name is valid; otherwise, false.</returns>
        public static bool IsValidRestaurantName(string name) =>
            !string.IsNullOrWhiteSpace(name) && name.Any(c => !char.IsWhiteSpace(c));

        /// <summary>
        /// Validates an item price input to ensure it is a decimal between 0 and 999.99, does not contain a '$', and is formatted correctly.
        /// </summary>
        /// <param name="input">The price input string.</param>
        /// <param name="price">The parsed price if valid.</param>
        /// <returns>True if the price is valid; otherwise, false.</returns>
        public static bool IsValidItemPrice(string input, out decimal price)
        {
            price = 0m;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            string cleanedInput = input.Trim();

            // Price should not contain a dollar sign
            if (cleanedInput.Contains('$'))
                return false;
            // Regex: 1-3 digits, optional . and 1-2 digits
            if (!Regex.IsMatch(cleanedInput, @"^\d{1,3}(\.\d{1,2})?$"))
                return false;

            if (!decimal.TryParse(cleanedInput, out price))
                return false;
            return price >= 0m && price <= 999.99m;
        }
    }
}