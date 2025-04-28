using EnsolversChallenge.Services;
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

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveNotes()
        {
            var notes = await _noteService.GetActiveNotes();
            return Ok(notes);
        }
    }
}
