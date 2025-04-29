using System.Threading.Tasks;
using EnsolversChallenge.Services;
using EnsolversChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EnsolversChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            try
            {
                var createNote = await _noteService.AddNote(note);
                return CreatedAtAction(nameof(GetNoteById), new { id = createNote.Id }, createNote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] NoteUpdateDto dto)
        {
            try
            {
                var updated = await _noteService.UpdateNote(id, dto);
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
    }
}
