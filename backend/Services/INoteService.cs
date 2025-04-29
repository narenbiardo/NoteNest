using EnsolversChallenge.Models;

namespace EnsolversChallenge.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetActiveNotes();
        Task<List<Note>> GetArchivedNotes();
        Task<Note?> GetNoteById(int id);
        Task<Note> AddNote(Note note);
        Task<Note> UpdateNote(int id, NoteUpdateDto dto);
        Task<bool> DeleteNote(int id);
        Task<Note?> ArchiveNote(int id);
        Task<Note?> UnarchiveNote(int id);
    }
}
