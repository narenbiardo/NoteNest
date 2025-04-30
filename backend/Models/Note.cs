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

        private string _title = default!;
        [Required]
        [StringLength(50)]
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty");
                _title = value;
            }
        }

        private string _content = default!;
        [Required]
        [StringLength(3000)]
        public string Content
        {
            get => _content;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Content cannot be empty");
                _content = value;
            }
        }

        public bool IsArchived { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public int UserId { get; set; }

        public User User { get; set; } = default!;
    }
}
