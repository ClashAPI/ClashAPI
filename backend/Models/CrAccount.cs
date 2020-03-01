using System;

namespace backend.Models
{
    public class CrAccount
    {
        public CrAccount()
        {
            CreatedAt = DateTime.Now;
            ExpiresAt = DateTime.Now.AddDays(1);
        }

        public int Id { get; set; }
        public string PlayerId { get; set; }
        public virtual User User { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime VerifiedAt { get; set; }
    }
}