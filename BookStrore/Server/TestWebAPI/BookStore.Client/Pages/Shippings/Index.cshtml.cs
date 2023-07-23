using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Data.Entities;
using BookStore.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Client.Pages.Shippings
{
    public class IndexModel : PageModel
    {
        private readonly BookStore.Data.BookStoreContext _context;

        public IndexModel(BookStore.Data.BookStoreContext context)
        {
            _context = context;
        }

        public IList<Shipping> Shipping { get;set; } = default!;

        public async Task OnGetAsync()
        {
			var statusList = Enum.GetValues(typeof(ShippingStatus))
				   .Cast<ShippingStatus>()
				   .Select(e => new SelectListItem
				   {
					   Value = ((int)e).ToString(),
					   Text = e.ToString()
				   }).ToList();
			ViewData["Status"] = statusList;
			if (_context.Shippings != null)
            {
                Shipping = await _context.Shippings.ToListAsync();
            }
        }
    }
}
