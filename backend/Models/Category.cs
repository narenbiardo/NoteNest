using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnsolversChallenge.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public required string Name { get; set; }

        [Required]
        [StringLength(50)]
        public required string Description { get; set; } 

        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
