using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int currentPage, int totalCount, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            PageSize = pageSize;
            TotalCount = totalCount;
            AddRange(items);
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, pageNumber, totalCount, pageSize);
        }
    }
}