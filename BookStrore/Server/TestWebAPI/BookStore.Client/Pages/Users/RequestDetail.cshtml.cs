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
	public class RequestDetailModel : PageModel
	{
		private readonly BookStore.Data.BookStoreContext _context;

		public RequestDetailModel(BookStore.Data.BookStoreContext context)
		{
			_context = context;
		}

		public BookBorrowingRequest BookBorrowingRequest { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.BookBorrowingRequests == null)
			{
				return NotFound();
			}

			var bookborrowingrequest = await _context.BookBorrowingRequests.FirstOrDefaultAsync(m => m.BookBorrowingRequestId == id);
			if (bookborrowingrequest == null)
			{
				return NotFound();
			}
			else
			{
				BookBorrowingRequest = bookborrowingrequest;
			}
			return Page();
		}
	}
}
