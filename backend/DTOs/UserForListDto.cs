using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.DTOs
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<CrAccount> CrAccounts { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
    }
}