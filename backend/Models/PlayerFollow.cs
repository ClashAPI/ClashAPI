using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class PlayerFollow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PlayerTag { get; set; }
        public string PlayerName { get; set; }
        public virtual User Follower { get; set; }
    }
}