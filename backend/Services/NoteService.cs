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

        public async Task<List<Note>> GetArchivedNotes()
        {
            return await _repository.GetArchivedNotes();
        }

        public async Task<Note?> GetNoteById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Note> UpdateNote(int id, NoteUpdateDto dto)
        {
            var existingNote = await _repository.GetById(id);

            if(existingNote == null)
            {
                throw new KeyNotFoundException($"No note found with ID {id}");
            }
            else
            {
                existingNote.Title = dto.Title;
                existingNote.Content = dto.Content;
                return await _repository.Update(existingNote);
            }
        }

        public async Task<Note?> ArchiveNote(int id)
        {
            return await _repository.UpdateArchiveStatus(id, true);
        }

        public async Task<Note?> UnarchiveNote(int id)
        {
            return await _repository.UpdateArchiveStatus(id, false);
        }
    }
}
