using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get;private set; }
        public int  CurrentPage { get; private set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;

        public PagedList(List<T> items,int pageSize,int pageNo,int totalCount)
        {
            TotalCount = totalCount;
            TotalPages =(int)Math.Ceiling(totalCount / (double)pageSize);
            PageSize = pageSize;
            CurrentPage = pageNo;
            AddRange(items);

        }

        public static  PagedList<T> Create(IQueryable<T> items,int pageSize,int currentPage)
        {
            var count = items.Count();
            var list = items.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();
            return new PagedList<T>(list, pageSize, currentPage, count);
        }

    }
}
