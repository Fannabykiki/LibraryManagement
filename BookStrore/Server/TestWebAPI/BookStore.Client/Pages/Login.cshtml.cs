using BookStore.API.DTOs.User;
using BookStore.Common.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookStore.Client.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public LoginRequest Login { get; set; }
        public string JwtToken { get; set; }
        public LoginResponse loginResponse { get; set; }
        public async Task<IActionResult> OnPost(LoginRequest loginRequest)
        {

            var client = new HttpClient();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var response = await client.PostAsJsonAsync("https://localhost:7233/api/authentication/token", Login);

            JwtToken = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
                var response_info = await client.GetAsync("https://localhost:7233/api/authentication/current-user");
                var content_info = await response_info.Content.ReadAsStringAsync();
                // Store the JWT token in a cookie or in local storage, for example:
                loginResponse = JsonSerializer.Deserialize<LoginResponse>(content_info, options);
                Response.Cookies.Append("jwt", JwtToken);
                HttpContext.Session.SetString("user", JsonSerializer.Serialize(loginResponse.UserName));
                HttpContext.Session.SetString("role", JsonSerializer.Serialize(loginResponse.Role));
                HttpContext.Session.SetString("userId", JsonSerializer.Serialize(loginResponse.UserId));
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }
    }
}
