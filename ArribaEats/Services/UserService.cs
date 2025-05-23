using ArribaEats.Models;
using ArribaEats.Repositories;

/// <summary>
/// Provides user-related services such as authentication and user management.
/// </summary>
public static class UserService
{
    private static readonly UserRepository _repository = new();

    /// <summary>
    /// Checks if an email is already registered.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <returns>True if the email exists; otherwise, false.</returns>
    public static bool EmailExists(string email) => _repository.IsEmailRegistered(email);

    /// <summary>
    /// Adds a new user to the repository.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public static void AddUser(User user) => _repository.AddUser(user);

    /// <summary>
    /// Retrieves a user by email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>The user with the specified email, or null if not found.</returns>
    public static User GetUserByEmail(string email) => _repository.GetUserByEmail(email);

    /// <summary>
    /// Authenticates a user by email and password.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The authenticated user.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if authentication fails.</exception>
    public static User Authenticate(string email, string password)
    {
        // Attempt to authenticate the user
        var user = _repository.Authenticate(email, password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        return user;
    }
}