using BookStore.Data;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Services.UserService
{
    public class UsersService : IUsersService
    {
        private readonly BookStoreContext _context;
        private readonly IUserRepository _userRepository;
        public UsersService(BookStoreContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<User> LoginUser(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(user => user.UserId == id);

            if (user == null || id == null) return null;

            return new User
            {
                UserId = user.UserId,
                Role = user.Role
            };
        }
    }
}
