using System;
using backend.Models;

namespace backend.DTOs
{
    public class CreatePostDto
    {
        public CreatePostDto()
        {
            CreatedAt = DateTime.Now;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public virtual User Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}