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
    public class DetailsModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public DetailsModel(BookStore.Data.BookStoreContext context)
        {
            _context = context;
        }

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
    }
}
