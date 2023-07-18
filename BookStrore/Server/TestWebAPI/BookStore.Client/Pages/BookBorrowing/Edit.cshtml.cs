using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Data.Entities;
using BookStore.Common.Enums;

namespace BookStore.Client.Pages.BookBorrowing
{
    public class EditModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public EditModel(BookStore.Data.BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookBorrowingRequest BookBorrowingRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BookBorrowingRequests == null)
            {
                return NotFound();
            }

            var bookborrowingrequest =  await _context.BookBorrowingRequests.FirstOrDefaultAsync(m => m.BookBorrowingRequestId == id);
            if (bookborrowingrequest == null)
            {
                return NotFound();
            }
			var statusList = Enum.GetValues(typeof(RequestStatusEnum))
					 .Cast<RequestStatusEnum>()
					 .Select(e => new SelectListItem
					 {
						 Value = ((int)e).ToString(),
						 Text = e.ToString()
					 }).ToList();
			ViewData["Status"] = statusList;
			BookBorrowingRequest = bookborrowingrequest;
           ViewData["UserRquestId"] = new SelectList(_context.Users, "UserId", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookBorrowingRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookBorrowingRequestExists(BookBorrowingRequest.BookBorrowingRequestId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookBorrowingRequestExists(int id)
        {
          return (_context.BookBorrowingRequests?.Any(e => e.BookBorrowingRequestId == id)).GetValueOrDefault();
        }
    }
}
