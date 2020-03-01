using backend.Models;

namespace backend.DTOs
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual User Author { get; set; }
        public bool IsVisible { get; set; }
    }
}