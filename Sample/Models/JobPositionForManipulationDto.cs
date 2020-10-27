using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sample.Entities;
using Sample.ValidationAttributes;

namespace Sample.Models
{
    [JobPositionTitleMustBeDifferentFromDescription(ErrorMessage =
        "Title must be different from description")]
    public abstract class JobPositionForManipulationDto
    { 
        [Required(ErrorMessage = "you must fill out Degree")]
        public Degree Degree { get; set; }

        [Required(ErrorMessage = "you must fill out title ")]
        [MaxLength(50,ErrorMessage = "max length of Title is 50 ")]
        public string Title { get; set; }

        [MaxLength(200,ErrorMessage = "max length of Description is 200")]
        public virtual string Description { get; set; }
    }
}
