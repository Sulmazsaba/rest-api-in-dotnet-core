using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ResourceParameters
{
    public class CompaniesResourceParameters
    {
        private const int maxPageSize = 10;
        public string SearchQuery { get; set; }

        private int _pageSize = 10;

        public int PageSize {
            get
            {
                return _pageSize;
            }
            set
            {
               _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }

        }

        public int PageNumber { get; set; } = 1;

        public string OrderBy { get; set; }
    }
}
