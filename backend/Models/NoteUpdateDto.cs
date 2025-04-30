using System.ComponentModel.DataAnnotations;

namespace EnsolversChallenge.Models
{
    public class NoteUpdateDto
    {
        [Required]
        public int NoteId { get; set; } = default!;

        [Required]
        public int UserId { get; set; } = default!;

        [Required, StringLength(50)]
        public string Title { get; set; } = default!;

        [Required, StringLength(3000)]
        public string Content { get; set; } = default!;
    }
}
