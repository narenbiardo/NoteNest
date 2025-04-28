using EnsolversChallenge.Data;
using EnsolversChallenge.Models;

namespace EnsolversChallenge.Repositories
{
    public interface INoteRepository
    {
        Task<List<Note>> GetActiveNotes();
        Task<List<Note>> GetArchivedNotes();
        Task<Note?> GetById(int id);
        Task<Note> Add(Note note);
        Task<Note> Update(Note note);
        Task Delete(int id);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;


        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Note> Add(Note note)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> GetActiveNotes()
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> GetArchivedNotes()
        {
            throw new NotImplementedException();
        }

        public Task<Note?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> Update(Note note)
        {
            throw new NotImplementedException();
        }
    }

}
