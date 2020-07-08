using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}