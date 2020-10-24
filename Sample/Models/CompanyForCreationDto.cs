using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class CompanyForCreationDto
    {
        public string Name { get; set; }
        public string Activity { get; set; }
        public int  NumberOfStaff { get; set; }
        public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;
    }
}
