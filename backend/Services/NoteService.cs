using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;

namespace EnsolversChallenge.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;

        public NoteService(INoteRepository repository)
        {
            _repository = repository;
        }

        public Task<Note> AddNote(Note note)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNote(int id)
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

        public Task<Note?> GetNoteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
