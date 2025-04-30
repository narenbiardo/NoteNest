using EnsolversChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsolversChallenge.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task<Category> Create(Category category);
        Task<Category?> Update(Category category);
        Task<bool> Delete(int id);
    }
}