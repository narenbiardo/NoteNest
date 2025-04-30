using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;

namespace EnsolversChallenge.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repo) => _userRepository = repo;

        public async Task<User> Register(string username, string password)
        {
            if (await _userRepository.GetByUsername(username) != null)
                throw new ArgumentException("Username already taken");

            var user = new User
            {
                Username = username,
                PasswordHash = User.HashPassword(password)
            };
            return await _userRepository.Add(user);
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user == null) return null;

            return User.VerifyPassword(user.PasswordHash, password) ? user : null;
        }

        public Task<List<Note>> GetUserNotes(int userId)
            => _userRepository.GetNotesByUser(userId);
    }
}
