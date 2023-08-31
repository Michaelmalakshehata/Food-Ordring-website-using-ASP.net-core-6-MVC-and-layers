using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Common.Pagination
{
    public class Pager
    {
        public int TotalItems { private set; get; }
        public int CurrentPage { private set; get; }
        public int PageSize { private set; get; }
        public int TotalPages { private set; get; }
        public int EndPage { private set; get; }
        public int StartPage { private set; get; }
        public Pager()
        {

        }
        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
