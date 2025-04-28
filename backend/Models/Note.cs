namespace EnsolversChallenge.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
