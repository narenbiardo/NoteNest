using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EnsolversChallenge.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryRepository _categoryRepository;

        public NoteService(
            INoteRepository noteRepository,
            ICategoryRepository categoryRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _noteRepository = noteRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Get userId from JWT
        private int CurrentUserId => int.Parse(
            _httpContextAccessor.HttpContext!
                .User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<Note> AddNote(CreateNoteDto createNoteDto)
        {
            if (string.IsNullOrWhiteSpace(createNoteDto.Title))
            {
                throw new ArgumentException("Title cannot be empty");
            }
            else if (string.IsNullOrWhiteSpace(createNoteDto.Content))
            {
                throw new ArgumentException("Content cannot be empty");
            }
            else
            {
                var note = new Note
                {
                    Title = createNoteDto.Title,
                    Content = createNoteDto.Content,
                    UserId = CurrentUserId,
                };
                return await _noteRepository.Add(note);
            }  
        }

        public async Task<bool> DeleteNote(int noteId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null)
            {
                throw new FileNotFoundException($"No note with ID:{noteId} found");
            }
            else if(note.UserId != CurrentUserId)
            {
                throw new UnauthorizedAccessException($"Cannot delete another user's note");
            }
            else
            {
                await _noteRepository.Delete(noteId);
                return true;
            }
                    
        }

        public async Task<List<Note>> GetActiveNotes()
        {
            var all = await _noteRepository.GetActiveNotes();
            return all.Where(n => n.UserId == CurrentUserId).ToList();
        }

        public async Task<List<Note>> GetArchivedNotes()
        {
            var all = await _noteRepository.GetArchivedNotes();
            return all.Where(n => n.UserId == CurrentUserId).ToList();
        }

        public async Task<Note?> GetNoteById(int noteId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null || note.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot access another user's note");
            return note;
        }

        public async Task<Note> UpdateNote(NoteUpdateDto dto)
        {
            var existingNote = await _noteRepository.GetById(dto.NoteId);

            if(existingNote == null)
            {
                throw new KeyNotFoundException($"No note found with ID {dto.NoteId}");
            }
            else if(existingNote.UserId != CurrentUserId)
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

        public async Task<Note?> ArchiveNote(int noteId)
        {
            var existingNote = await _noteRepository.GetById(noteId);
            if (existingNote == null || existingNote.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot modify another user's note");
            return await _noteRepository.UpdateArchiveStatus(noteId, true);
        }

        public async Task<Note?> UnarchiveNote(int noteId)
        {
            var existingNote = await _noteRepository.GetById(noteId);
            if (existingNote == null || existingNote.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot modify another user's note");
            return await _noteRepository.UpdateArchiveStatus(noteId, false); ;
        }

        public async Task<List<Category>> GetNoteCategories(int noteId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null)
                throw new FileNotFoundException($"No note with ID:{noteId} found");
            else if (note.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot acess another user's notes");
            else
                return await _noteRepository.GetNoteCategories(noteId);
        }

        public async Task<bool> AddCategoryToNote(int noteId, int categoryId)
        {
            var note = await _noteRepository.GetById(noteId);
            var category = await _categoryRepository.GetById(categoryId);

            if (note == null)
                throw new FileNotFoundException($"No note with ID:{noteId} found");
            else if (note.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot acess another user's notes");
            else if (category == null)
                throw new FileNotFoundException($"No category with ID:{categoryId} found");
            else if (category.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot acess another user's category");
            else
            {
                await _noteRepository.AddCategoryToNote(noteId, categoryId);
                return true;
            }
                
        }

        public async Task<bool> RemoveCategoryFromNote(int noteId, int categoryId)
        {
            var note = await _noteRepository.GetById(noteId);
            var category = await _categoryRepository.GetById(categoryId);

            if (note == null)
                throw new FileNotFoundException($"No note with ID:{noteId} found");
            else if (note.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot acess another user's notes");
            else if (category == null)
                throw new FileNotFoundException($"No category with ID:{categoryId} found");
            else if (category.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot acess another user's category");
            else
            {
                await _noteRepository.RemoveCategoryFromNote(noteId, categoryId);
                return true;
            }
        }
    }
}
