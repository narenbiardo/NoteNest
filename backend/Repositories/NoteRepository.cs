using EnsolversChallenge.Data;
using EnsolversChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsolversChallenge.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;


        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Note> Add(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Note>> GetActiveNotes()
        {
            return await _context.Notes
                .Where(n => !n.IsArchived)
                .OrderBy(n => n.CreateDate)
                .ToListAsync();
        }

        public Task<List<Note>> GetArchivedNotes()
        {
            throw new NotImplementedException();
        }

        public async Task<Note?> GetById(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Note> Update(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return note;
        }
    }

}
