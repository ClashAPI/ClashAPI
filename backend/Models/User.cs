using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            PlayerFollows = new List<PlayerFollow>();
            ClanFollows = new List<ClanFollow>();
        }

        public virtual List<CrAccount> CrAccounts { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual IList<PlayerFollow> PlayerFollows { get; set; }
        public virtual IList<ClanFollow> ClanFollows { get; set; }
    }
}