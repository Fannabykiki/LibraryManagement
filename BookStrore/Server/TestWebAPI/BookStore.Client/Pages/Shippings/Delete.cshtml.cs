using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Data.Entities;

namespace BookStore.Client.Pages.Shippings
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public DeleteModel(BookStore.Data.BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Shipping Shipping { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shippings == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings.FirstOrDefaultAsync(m => m.ShippingId == id);

            if (shipping == null)
            {
                return NotFound();
            }
            else 
            {
                Shipping = shipping;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Shippings == null)
            {
                return NotFound();
            }
            var shipping = await _context.Shippings.FindAsync(id);

            if (shipping != null)
            {
                Shipping = shipping;
                _context.Shippings.Remove(Shipping);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
