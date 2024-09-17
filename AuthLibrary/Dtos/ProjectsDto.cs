using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthLibrary.Dtos
{
    public class ProjectsDto
    {

        public int Project_Id { get; set; }

        [Required]
        public string Project_Name { get; set; }

        [Required]
        public string Description { get; set; }

        /*[JsonConverter(typeof(DataLibrary.DataFormat.DateConvert))]*/
        //[RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "Date must be in the format MM/dd/yyyy")]
        public DateTime? DueDate { get; set; }
    }
}
