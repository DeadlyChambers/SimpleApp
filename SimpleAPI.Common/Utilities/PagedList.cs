using System;
using System.Collections.Generic;

namespace SimpleAPI.Common.Utilities
{
    public class PagedList<T> : IPage
    {
        public PagedList()
        {

        }
        public PagedList(IPage page)
        {
            PageSize = page.PageSize;
            CurrentPage = page.CurrentPage;
            TotalRecords = page.TotalRecords;
            TotalPages = page.TotalPages;

            if (PageSize > 50)
                PageSize = 50;
            else if (PageSize < 1)
                PageSize = 1;

            TotalPages = (int)Math.Ceiling((decimal)TotalRecords / (decimal)PageSize);
            if (CurrentPage < 1)
                CurrentPage = 1;
            else if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;
        }

        public IList<T> Items { get; set; } = new List<T> { };
        public int PageSize { get; set; } = 4;
        public int CurrentPage { get; set; } = 1;
        public int TotalRecords { get; set; }

        public int TotalPages { get; set; } = 1;

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
    }

    public interface IPage
    {
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        int PageSize { get; set; }
        int CurrentPage { get; set; }
        int TotalRecords { get; set; }

        int TotalPages { get; set; }
    }
}
