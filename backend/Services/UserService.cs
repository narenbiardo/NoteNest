using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EnsolversChallenge.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Get userId from JWT
        private int CurrentUserId => int.Parse(
            _httpContextAccessor.HttpContext!
                .User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

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

        public Task<List<Note>> GetUserNotes()
            => _userRepository.GetNotesByUser(CurrentUserId);
    }
}
