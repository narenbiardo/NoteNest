using System.Threading.Tasks;
using EnsolversChallenge.Services;
using EnsolversChallenge.Models;
using Microsoft.AspNetCore.Mvc;

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
            var notes = await _noteService.GetActiveNotes();
            return Ok(notes);
        }
    }
}
