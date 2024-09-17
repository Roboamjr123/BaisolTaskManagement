using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class SubDetails
    {
        [Key]
        public int SubD_Id { get; set; }
        public string? SubD_Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [ForeignKey("SubTasks")]
        public int SubT_Id { get; set; }
        public SubTasks SubTask { get; set; }
    }
}
