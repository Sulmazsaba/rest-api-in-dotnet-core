using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Activity { get; set; }
        public int  NumberOfStaff { get; set; }
        public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;
        public ICollection<JobPosition>  JobPositions { get; set; }=new List<JobPosition>();
    }
}
