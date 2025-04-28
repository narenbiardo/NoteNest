using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsolversChallenge.Models
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(3000)]
        public string Content { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
