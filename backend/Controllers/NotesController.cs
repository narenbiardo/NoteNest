using System.Threading.Tasks;
using EnsolversChallenge.Services;
using EnsolversChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EnsolversChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            try
            {
                var note = await _noteService.GetNoteById(id);
                if (note == null) return NotFound();
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] CreateNoteDto createNoteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ValidationProblem(ModelState);
                else
                {
                    var created = await _noteService.AddNote(createNoteDto);
                    return CreatedAtAction(nameof(GetNoteById), new { id = created.Id }, created);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveNotes()
        {
            try
            {
                var notes = await _noteService.GetActiveNotes();
                return Ok(notes);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote([FromBody] NoteUpdateDto NoteUpdateDto)
        {
            try
            {
                var updated = await _noteService.UpdateNote(NoteUpdateDto);
                if (updated == null)
                    return NotFound();
                else
                {
                    return Ok(updated);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("archived")]
        public async Task<IActionResult> GetArchivedNotes()
        {
            try
            {
                var notes = await _noteService.GetArchivedNotes();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> ArchiveNote(int id)
        {
            try
            {
                var note = await _noteService.ArchiveNote(id);
                return note != null ? Ok(note) : NotFound();
            }
            catch( Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}/unarchive")]
        public async Task<IActionResult> UnarchiveNote(int id)
        {
            try
            {
                var note = await _noteService.UnarchiveNote(id);
                return note != null ? Ok(note) : NotFound();
            }
            catch ( Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                var result = await _noteService.DeleteNote(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{noteId}/categories")]
        public async Task<IActionResult> GetNoteCategories(int noteId)
        {
            try
            {
                var categories = await _noteService.GetNoteCategories(noteId);
                return Ok(categories);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{noteId}/categories/{categoryId}")]
        public async Task<IActionResult> AddCategoryToNote(int noteId, int categoryId)
        {
            try
            {
                var result = await _noteService.AddCategoryToNote(noteId, categoryId);
                return result ? NoContent() : NotFound();
            }
            catch( Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{noteId}/categories/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryFromNote(int noteId, int categoryId)
        {
            try
            {
                var result = await _noteService.RemoveCategoryFromNote(noteId, categoryId);
                return result ? NoContent() : NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
