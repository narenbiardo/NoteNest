using EnsolversChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsolversChallenge.Services
{
    public interface ICategoryService
    {
        Task<Category?> GetById(int categoryId);
        Task<Category> Create(CreateCategoryDto createCategoryDto);
        Task<Category?> Update(UpdateCategoryDto updateCategoryDto);
        Task<bool> Delete(int categoryId);
        Task<IEnumerable<Note>> GetNotesByCategory(int categoryId);
    }
}