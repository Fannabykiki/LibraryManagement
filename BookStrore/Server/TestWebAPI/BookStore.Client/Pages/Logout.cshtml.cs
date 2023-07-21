using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Client.Pages
{
	public class LogoutModel : PageModel
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LogoutModel(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult OnGet()
		{
			// Clear the authentication token from the client-side storage
			Response.Cookies.Delete("jwt");

			HttpContext.Session.Remove("role");

			HttpContext.Session.Remove("user");

			// Redirect the user to the login page
			return RedirectToPage("/Index");
		}
	}
}
