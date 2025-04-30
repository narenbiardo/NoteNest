using EnsolversChallenge.Models;

namespace EnsolversChallenge.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByUsername(string username);
        Task<User> Add(User user);
        Task<List<Note>> GetNotesByUser(int userId);
    }
}
