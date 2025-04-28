using EnsolversChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsolversChallenge.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes => Set<Note>();
    }
}
