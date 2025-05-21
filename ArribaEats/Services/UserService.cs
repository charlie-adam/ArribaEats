using ArribaEats.Models;
using ArribaEats.Repositories;

public static class UserService
{
    private static readonly UserRepository _repository = new();

    public static bool EmailExists(string email) => _repository.IsEmailRegistered(email);
    public static void AddUser(User user) => _repository.AddUser(user);
    public static User GetUserByEmail(string email) => _repository.GetUserByEmail(email);
}