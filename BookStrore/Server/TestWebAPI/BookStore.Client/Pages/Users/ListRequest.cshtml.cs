using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Data.Entities;

namespace BookStore.Client.Pages.Users
{
    public class ListRequestModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public ListRequestModel(BookStore.Data.BookStoreContext context)
        {
            _context = context;
        }

        public IList<BookBorrowingRequest> BookBorrowingRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BookBorrowingRequests != null)
            {
                BookBorrowingRequest = await _context.BookBorrowingRequests
                .Include(b => b.User).ToListAsync();
            }
        }
    }
}
