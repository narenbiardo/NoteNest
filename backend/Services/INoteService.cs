using EnsolversChallenge.Models;

namespace EnsolversChallenge.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetActiveNotes();
        Task<List<Note>> GetArchivedNotes();
        Task<Note?> GetNoteById(int noteId);
        Task<Note> AddNote(CreateNoteDto createNoteDto);
        Task<Note> UpdateNote(NoteUpdateDto noteUpdateDto);
        Task<bool> DeleteNote(int noteId);
        Task<Note?> ArchiveNote(int noteId);
        Task<Note?> UnarchiveNote(int noteId);
        Task<List<Category>> GetNoteCategories(int noteId);
        Task<bool> AddCategoryToNote(int noteId, int categoryId);
        Task<bool> RemoveCategoryFromNote(int noteId, int categoryId);
    }
}
