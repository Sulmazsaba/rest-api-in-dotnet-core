using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class JobPositionForUpdateDto :JobPositionForManipulationDto
    {
        [Required(ErrorMessage = "Description must be filled out")]
        public override string Description { get; set; }
    }
}
