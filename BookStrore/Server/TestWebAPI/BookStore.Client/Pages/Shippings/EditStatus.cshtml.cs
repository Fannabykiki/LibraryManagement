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

namespace BookStore.Client.Pages.Shippings
{
    public class EditStatusModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public EditStatusModel(BookStore.Data.BookStoreContext context)
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
            var statusList = Enum.GetValues(typeof(ShippingStatus))
                     .Cast<ShippingStatus>()
                     .Select(e => new SelectListItem
                     {
                         Value = ((int)e).ToString(),
                         Text = e.ToString()
                     }).ToList();
            ViewData["Status"] = statusList;
            var shipping =  await _context.Shippings.FirstOrDefaultAsync(m => m.ShippingId == id);
            if (shipping == null)
            {
                return NotFound();
            }
            Shipping = shipping;
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

            _context.Attach(Shipping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingExists(Shipping.ShippingId))
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

        private bool ShippingExists(int id)
        {
          return (_context.Shippings?.Any(e => e.ShippingId == id)).GetValueOrDefault();
        }
    }
}
