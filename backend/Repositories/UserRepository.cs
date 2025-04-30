using EnsolversChallenge.Data;
using EnsolversChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsolversChallenge.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task<User?> GetById(int id)
            => await _context.Users.FindAsync(id);

        public async Task<User?> GetByUsername(string username)
            => await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

        public async Task<User> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<Note>> GetNotesByUser(int userId)
            => await _context.Notes
                 .Where(n => n.UserId == userId)
                 .ToListAsync();
    }
}
