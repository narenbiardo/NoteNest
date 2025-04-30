using System.ComponentModel.DataAnnotations;

namespace EnsolversChallenge.Models
{
    public class UpdateCategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; } = default!;

        [StringLength(50)]
        public string? Description { get; set; }
    }
}
