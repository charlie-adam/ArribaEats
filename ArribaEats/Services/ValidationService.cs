using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArribaEats.Services
{
    public static class ValidationService
    {
        // Name must contain only letters, spaces, apostrophes and hyphens, and at least one letter.
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            // Check that all chars are allowed
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s'\-]+$")) return false;
            // Check at least one letter
            return name.Any(char.IsLetter);
        }

        // Age must be int between 18 and 100 inclusive
        public static bool IsValidAge(string input, out int age) =>
            int.TryParse(input, out age) && age is >= 18 and <= 100;

        // Email must have exactly one '@' and at least one char before and after
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var parts = email.Split('@');
            if (parts.Length != 2) return false;
            if (string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1])) return false;
            return true;
        }

        // Phone must be exactly 10 digits and start with '0'
        public static bool IsValidPhone(string phone) =>
            phone != null &&
            phone.Length == 10 &&
            phone.All(char.IsDigit) &&
            phone.StartsWith("0");

        // Password must be at least 8 chars with lowercase, uppercase and digit
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            return password.Length >= 8 &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit);
        }

        // Location input must be "X,Y" with both X and Y as integers
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

        // Licence Plate: 1-8 characters, uppercase letters, digits, spaces, not all spaces
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

        // Food Style: numeric 1 to 6 inclusive
        public static bool IsValidFoodStyle(string input, out int style)
        {
            if (int.TryParse(input, out style))
            {
                return style >= 1 && style <= 6;
            }
            return false;
        }

        // Restaurant name must have at least one non-whitespace character
        public static bool IsValidRestaurantName(string name) =>
            !string.IsNullOrWhiteSpace(name) && name.Any(c => !char.IsWhiteSpace(c));

        // Item price must be between 0.00 and 999.99 inclusive
        public static bool IsValidItemPrice(string input, out decimal price)
        {
            price = 0m;
            if (string.IsNullOrWhiteSpace(input)) return false;

            // Remove any currency symbols or whitespace
            string cleanedInput = input.Trim().TrimStart('$');

            if (decimal.TryParse(cleanedInput, out price))
            {
                return price >= 0m && price <= 999.99m;
            }
            return false;
        }
    }
}