using System.ComponentModel.DataAnnotations;

namespace EnsolversChallenge.Models
{
    public class CreateCategoryDto
    {
        [Required, StringLength(25)]
        public string Name { get; set; } = default!;

        [StringLength(50)]
        public string? Description { get; set; }
    }
}
