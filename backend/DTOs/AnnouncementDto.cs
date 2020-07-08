using System;
using backend.Models;

namespace backend.DTOs
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}