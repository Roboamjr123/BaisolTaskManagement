using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Projects
    {
        [Key]
        public int Project_Id { get; set; }
        public string? Project_Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<Tasks> Task { get; set; }
    }
}
