using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnsolversChallenge.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository, ICategoryRepository categoryRepository)
        {
            _noteRepository = noteRepository;
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
                return await _noteRepository.Add(note);
            }  
        }

        public async Task<bool> DeleteNote(int id)
        {
            var note = await _noteRepository.GetById(id);
            if (note == null)
            {
                return false;
            }
            else
            {
                await _noteRepository.Delete(id);
                return true;
            }
        }

        public async Task<List<Note>> GetActiveNotes()
        {
            return await _noteRepository.GetActiveNotes();
        }

        public async Task<List<Note>> GetArchivedNotes()
        {
            return await _noteRepository.GetArchivedNotes();
        }

        public async Task<Note?> GetNoteById(int id)
        {
            return await _noteRepository.GetById(id);
        }

        public async Task<Note> UpdateNote(NoteUpdateDto dto)
        {
            var existingNote = await _noteRepository.GetById(dto.NoteId);

            if(existingNote == null)
            {
                throw new KeyNotFoundException($"No note found with ID {dto.NoteId}");
            }
            else if(existingNote.UserId != dto.UserId)
            {
                throw new UnauthorizedAccessException($"Cannot modify another user's note");
            }
            else
            {
                existingNote.Title = dto.Title;
                existingNote.Content = dto.Content;
                return await _noteRepository.Update(existingNote);
            }
        }

        public async Task<Note?> ArchiveNote(int id)
        {
            return await _noteRepository.UpdateArchiveStatus(id, true);
        }

        public async Task<Note?> UnarchiveNote(int id)
        {
            return await _noteRepository.UpdateArchiveStatus(id, false);
        }

        public async Task<List<Category>> GetNoteCategories(int noteId)
        {
            return await _noteRepository.GetNoteCategories(noteId);
        }

        public async Task<bool> AddCategoryToNote(int noteId, int categoryId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null) return false;

            var category = await _noteRepository.GetById(categoryId);
            if (category == null) return false;

            await _noteRepository.AddCategoryToNote(noteId, categoryId);
            return true;
        }

        public async Task<bool> RemoveCategoryFromNote(int noteId, int categoryId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null) return false;

            var category = await _noteRepository.GetById(categoryId);
            if (category == null) return false;

            await _noteRepository.RemoveCategoryFromNote(noteId, categoryId);
            return true;
        }
    }
}
