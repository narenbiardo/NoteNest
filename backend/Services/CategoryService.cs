using EnsolversChallenge.Models;
using EnsolversChallenge.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnsolversChallenge.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryService(
                ICategoryRepository categoryRepository,
                IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // userId from JWT
        private int CurrentUserId =>
            int.Parse(_httpContextAccessor.HttpContext!
                .User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<IEnumerable<Category>> GetAll()
        {
            var all = await _categoryRepository.GetAll();
            return all.Where(c => c.UserId == CurrentUserId);
        }
        public async Task<Category?> GetById(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new FileNotFoundException($"No category with ID:{categoryId} found");
            else if (category.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot access another user's category");
            else
                return category;
        }
        public async Task<Category> Create(CreateCategoryDto createCategoryDtodto)
        {
            if (string.IsNullOrWhiteSpace(createCategoryDtodto.Name))
                throw new ArgumentException("Name cannot be empty");
            else if (string.IsNullOrWhiteSpace(createCategoryDtodto.Description))
                throw new ArgumentException("Description cannot be empty");
            else
            {
                var category = new Category
                {
                    Name = createCategoryDtodto.Name,
                    Description = createCategoryDtodto.Description,
                    UserId = CurrentUserId
                };
                return await _categoryRepository.Add(category);
            }
                
        }
        public async Task<Category?> Update(UpdateCategoryDto updateCategorycategoryDto)
        {
            var category = await _categoryRepository.GetById(updateCategorycategoryDto.Id);
            if (category == null)
                throw new FileNotFoundException($"No category with ID:{updateCategorycategoryDto.Id} found");
            else if(category.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot modify another user's category");
            else
            {
                category.Name = updateCategorycategoryDto.Name;
                category.Description = updateCategorycategoryDto.Description!;
                return await _categoryRepository.Update(category);
            }     
        }
        public async Task<bool> Delete(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new FileNotFoundException($"No category with ID:{categoryId} found");
            else if (category.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot delete another user's category");
            else
                return await _categoryRepository.Delete(categoryId);           
        }
        public async Task<IEnumerable<Note>> GetNotesByCategory(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new FileNotFoundException($"No category with ID:{categoryId} found");
            else if (category.UserId != CurrentUserId)
                throw new UnauthorizedAccessException($"Cannot access another user's category");
            else
                return await _categoryRepository.GetNotesByCategory(categoryId);
        }
    }
}