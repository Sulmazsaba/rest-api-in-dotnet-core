using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Entities
{
    public class JobPosition
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Degree Degree { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)] 
        public string Description { get; set; }


        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

    }
}