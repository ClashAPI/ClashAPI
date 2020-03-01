using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.DTOs
{
    public class CreateAnnouncementDto
    {
        public string Subject { get; set; }
        public string Type { get; set; }
    }
}
