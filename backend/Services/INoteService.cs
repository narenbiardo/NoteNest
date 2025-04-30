using EnsolversChallenge.Models;

namespace EnsolversChallenge.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetActiveNotes();
        Task<List<Note>> GetArchivedNotes();
        Task<Note?> GetNoteById(int id);
        Task<Note> AddNote(Note note);
        Task<Note> UpdateNote(NoteUpdateDto dto);
        Task<bool> DeleteNote(int id);
        Task<Note?> ArchiveNote(int id);
        Task<Note?> UnarchiveNote(int id);
        Task<List<Category>> GetNoteCategories(int noteId);
        Task<bool> AddCategoryToNote(int noteId, int categoryId);
        Task<bool> RemoveCategoryFromNote(int noteId, int categoryId);
    }
}
