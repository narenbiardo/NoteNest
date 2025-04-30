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
        Task<Note?> UpdateArchiveStatus(int id, bool archiveStatus);
        Task<List<Category>> GetNoteCategories(int noteId);
        Task AddCategoryToNote(int noteId, int categoryId);
        Task RemoveCategoryFromNote(int noteId, int categoryId);
    }
}
