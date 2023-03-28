using System;
using System.Collections.Generic;
using System.Linq;

namespace APIHelpersLibrary.Paged
{
    public class PagedList<T> : List<T> where T : class
    {
        public MetaData MetaData { get; set; }
        public PagedList(ICollection<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                CurrentPage = pageNumber,
                TotalItems = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange( items );
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            var items=source
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();
                
            return new PagedList<T>(items,count, pageNumber, pageSize); 
        }
    }
}
