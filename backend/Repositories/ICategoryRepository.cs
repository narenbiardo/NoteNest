using EnsolversChallenge.Models;

namespace EnsolversChallenge.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetById(int id);
        Task<Category> Add(Category category);
        Task<Category?> Update(Category category);
        Task<bool> Delete(int id);
        Task<List<Note>> GetNotesByCategory(int categoryId);
        Task<List<Category>> GetByUser(int userId);
    }
}
