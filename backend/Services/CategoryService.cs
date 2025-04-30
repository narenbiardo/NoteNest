using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsolversChallenge.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository repo) => _categoryRepository = repo;

        public Task<List<Category>> GetAll() => _categoryRepository.GetAll();
        public Task<Category?> GetById(int id) => _categoryRepository.GetById(id);
        public Task<Category> Create(Category category) => _categoryRepository.Add(category);
        public Task<Category?> Update(Category category) => _categoryRepository.Update(category);
        public Task<bool> Delete(int id) => _categoryRepository.Delete(id);
        public Task<List<Note>> GetNotesByCategory(int categoryId) => _categoryRepository.GetNotesByCategory(categoryId);
    }
}