using System;
using backend.Models;

namespace backend.DTOs
{
    public class UpdatePostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual User Author { get; set; }
        public bool IsVisible { get; set; }
    }
}