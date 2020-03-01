using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public virtual User Author { get; set; }
        public DateTime CreatedAt { get; set; }

        public Announcement()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
