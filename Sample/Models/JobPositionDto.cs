﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Entities;

namespace Sample.Models
{
    public class JobPositionDto
    {
        
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Degree Degree { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
