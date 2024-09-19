using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary.Dtos
{
    public class SubTasksDto
    {
        public int SubT_Id { get; set; }
        public string? SubT_Name { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int? duration { get; set; }
        public int? progress { get; set; } = 0;

        public int Task_Id { get; set; }
    }
}
