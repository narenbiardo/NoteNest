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
    [Route("note")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{noteId}", Name = nameof(GetNoteById))]
        public async Task<IActionResult> GetNoteById([FromRoute] int noteId)
        {
            try
            {
                var note = await _noteService.GetNoteById(noteId);
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
                    return CreatedAtAction(nameof(GetNoteById), new { noteId = created.Id }, created);
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

        [HttpPut("{noteId}")]
        public async Task<IActionResult> UpdateNote([FromRoute] int noteId, [FromBody] NoteUpdateDto noteUpdateDto)
        {
            noteUpdateDto.NoteId = noteId;
            try
            {
                var updated = await _noteService.UpdateNote(noteUpdateDto);
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

        [HttpPatch("{noteId}/archive")]
        public async Task<IActionResult> ArchiveNote([FromRoute] int noteId)
        {
            try
            {
                var note = await _noteService.ArchiveNote(noteId);
                return note != null ? Ok(note) : NotFound();
            }
            catch( Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{noteId}/unarchive")]
        public async Task<IActionResult> UnarchiveNote([FromRoute] int noteId)
        {
            try
            {
                var note = await _noteService.UnarchiveNote(noteId);
                return note != null ? Ok(note) : NotFound();
            }
            catch ( Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{noteId}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int noteId)
        {
            try
            {
                var result = await _noteService.DeleteNote(noteId);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{noteId}/categories")]
        public async Task<IActionResult> GetNoteCategories([FromRoute] int noteId)
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

        [HttpPost("{noteId}/category/{categoryId}")]
        public async Task<IActionResult> AddCategoryToNote([FromRoute] int noteId, [FromRoute] int categoryId)
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

        [HttpDelete("{noteId}/category/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryFromNote([FromRoute] int noteId, [FromRoute] int categoryId)
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
