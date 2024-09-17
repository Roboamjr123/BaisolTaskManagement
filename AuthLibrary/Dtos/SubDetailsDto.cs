using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary.Dtos
{
    public class SubDetailsDto
    {
        public int SubD_Id { get; set; }
        public string? SubD_Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public int SubT_Id { get; set; }
    }
}
