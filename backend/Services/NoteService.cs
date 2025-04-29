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

        public async Task<Note> AddNote(Note note)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
            {
                throw new ArgumentException("Title cannot be empty");
            }
            else if (string.IsNullOrWhiteSpace(note.Content))
            {
                throw new ArgumentException("Content cannot be empty");
            }
            else
            {
                return await _repository.Add(note);
            }  
        }

        public Task DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Note>> GetActiveNotes()
        {
            return await _repository.GetActiveNotes();
        }

        public Task<List<Note>> GetArchivedNotes()
        {
            throw new NotImplementedException();
        }

        public async Task<Note?> GetNoteById(int id)
        {
            return await _repository.GetById(id);
        }

        public Task<Note> UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
