using EnsolversChallenge.Models;

namespace EnsolversChallenge.Services
{
    public interface IUserService
    {
        Task<User> Register(string username, string password);
        Task<User?> Authenticate(string username, string password);
        Task<List<Note>> GetUserNotes();
    }
}
