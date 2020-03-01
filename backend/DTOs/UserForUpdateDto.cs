using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.DTOs
{
    public class UserForUpdateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}