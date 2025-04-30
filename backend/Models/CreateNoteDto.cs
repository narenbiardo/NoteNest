using System.ComponentModel.DataAnnotations;

namespace EnsolversChallenge.Models
{
    public class CreateNoteDto
    {
        [Required, StringLength(50)]
        public required string Title { get; set; }

        [Required, StringLength(3000)]
        public required string Content { get; set; }
    }
}
