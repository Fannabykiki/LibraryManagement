using BookStore.Data;
using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Services.UserService
{
    public class UsersService : IUsersService
    {
        private readonly BookStoreContext _context;
        public UsersService(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<User> LoginUser(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }
    }
}
