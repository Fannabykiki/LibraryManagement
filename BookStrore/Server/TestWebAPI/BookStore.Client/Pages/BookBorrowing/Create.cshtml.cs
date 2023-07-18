using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Data;
using BookStore.Data.Entities;

namespace BookStore.Client.Pages.BookBorrowing
{
    public class CreateModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public CreateModel(BookStore.Data.BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserRquestId"] = new SelectList(_context.Users, "UserId", "UserName");
        ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName");
        return Page();
        }

        [BindProperty]
        public BookBorrowingRequest BookBorrowingRequest { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BookBorrowingRequests == null || BookBorrowingRequest == null)
            {
                return Page();
            }

            _context.BookBorrowingRequests.Add(BookBorrowingRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
