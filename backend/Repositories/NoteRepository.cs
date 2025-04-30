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

        public async Task Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Note>> GetActiveNotes()
        {
            return await _context.Notes
                .Where(n => !n.IsArchived)
                .OrderBy(n => n.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Note>> GetArchivedNotes()
        {
            return await _context.Notes
                .Where (n => n.IsArchived)
                .OrderByDescending(n => n.CreateDate)
                .ToListAsync();
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

        public async Task<Note?> UpdateArchiveStatus(int id, bool archiveStatus)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                note.IsArchived = archiveStatus;
                await _context.SaveChangesAsync();
            }
            return note;
        }

        public async Task<List<Category>> GetNoteCategories(int noteId)
        {
            return await _context.Notes
                .Where(n => n.Id == noteId)
                .SelectMany(n => n.Categories)
                .ToListAsync();
        }

        public async Task AddCategoryToNote(int noteId, int categoryId)
        {
            var note = await _context.Notes
                .Include(n => n.Categories)
                .FirstOrDefaultAsync(n => n.Id == noteId);

            var category = await _context.Categories.FindAsync(categoryId);

            if (note != null && category != null && !note.Categories.Contains(category))
            {
                note.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
        }
    }

}
