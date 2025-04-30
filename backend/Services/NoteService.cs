using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnsolversChallenge.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _Noterepository;
        private readonly ICategoryRepository _categoryRepository;

        public NoteService(INoteRepository noteRepository, ICategoryRepository categoryRepository)
        {
            _Noterepository = noteRepository;
            _categoryRepository = categoryRepository;
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
                return await _Noterepository.Add(note);
            }  
        }

        public async Task<bool> DeleteNote(int id)
        {
            var note = await _Noterepository.GetById(id);
            if (note == null)
            {
                return false;
            }
            else
            {
                await _Noterepository.Delete(id);
                return true;
            }
        }

        public async Task<List<Note>> GetActiveNotes()
        {
            return await _Noterepository.GetActiveNotes();
        }

        public async Task<List<Note>> GetArchivedNotes()
        {
            return await _Noterepository.GetArchivedNotes();
        }

        public async Task<Note?> GetNoteById(int id)
        {
            return await _Noterepository.GetById(id);
        }

        public async Task<Note> UpdateNote(int id, NoteUpdateDto dto)
        {
            var existingNote = await _Noterepository.GetById(id);

            if(existingNote == null)
            {
                throw new KeyNotFoundException($"No note found with ID {id}");
            }
            else
            {
                existingNote.Title = dto.Title;
                existingNote.Content = dto.Content;
                return await _Noterepository.Update(existingNote);
            }
        }

        public async Task<Note?> ArchiveNote(int id)
        {
            return await _Noterepository.UpdateArchiveStatus(id, true);
        }

        public async Task<Note?> UnarchiveNote(int id)
        {
            return await _Noterepository.UpdateArchiveStatus(id, false);
        }

        public async Task<List<Category>> GetNoteCategories(int noteId)
        {
            return await _Noterepository.GetNoteCategories(noteId);
        }

        public async Task<bool> AddCategoryToNote(int noteId, int categoryId)
        {
            var note = await _Noterepository.GetById(noteId);
            if (note == null) return false;

            var category = await _categoryRepository.GetById(categoryId);
            if (category == null) return false;

            await _Noterepository.AddCategoryToNote(noteId, categoryId);
            return true;
        }

        public async Task<bool> RemoveCategoryFromNote(int noteId, int categoryId)
        {
            var note = await _Noterepository.GetById(noteId);
            if (note == null) return false;

            var category = await _categoryRepository.GetById(categoryId);
            if (category == null) return false;

            await _Noterepository.RemoveCategoryFromNote(noteId, categoryId);
            return true;
        }
    }
}
