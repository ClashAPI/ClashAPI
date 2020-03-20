using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CrAccount
    {
        public CrAccount()
        {
            CreatedAt = DateTime.Now;
            ExpiresAt = DateTime.Now.AddDays(1);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PlayerId { get; set; }
        public virtual User User { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime VerifiedAt { get; set; }
    }
}