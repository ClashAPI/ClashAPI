using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Post
    {
        public Post()
        {
            IsVisible = true;
            UpdatedAt = DateTime.Now;
        }

        public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Content { get; set; }

        public virtual User Author { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}