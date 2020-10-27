using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sample.Models;

namespace Sample.ValidationAttributes
{
    public class JobPositionTitleMustBeDifferentFromDescriptionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dto = (JobPositionForManipulationDto) validationContext.ObjectInstance;
            if(dto.Title==dto.Description)
                return new ValidationResult(ErrorMessage,new []{nameof(JobPositionForManipulationDto)});
            return ValidationResult.Success;
        }
    }
}
