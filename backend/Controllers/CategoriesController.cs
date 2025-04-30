using EnsolversChallenge.Models;
using EnsolversChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EnsolversChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _categoryService.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{categoryId}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            try
            {
                var cat = await _categoryService.GetById(categoryId);
                return cat != null ? Ok(cat) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }      
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                else
                {
                    try
                    {
                        var created = await _categoryService.Create(createCategoryDto);
                        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
                    }
                    catch(Exception ex)
                    {
                        return BadRequest(ex.Message);  
                    }
                }
                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }   
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            updateCategoryDto.Id = categoryId;
            try
            {
                var updated = await _categoryService.Update(updateCategoryDto);
                return updated != null ? Ok(updated) : NotFound();     
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            try
            {
                var result = await _categoryService.Delete(categoryId);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{categoryId}/notes")]
        public async Task<IActionResult> GetNotesByCategory([FromRoute] int categoryId)
        {
            try
            {
                var notes = await _categoryService.GetNotesByCategory(categoryId);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
    }
}