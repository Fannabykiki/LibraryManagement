using BookStore.Data.Entities;

namespace BookStore.API.Services.UserService
{
    public interface IUsersService
    {
        Task<User> LoginUser(string username, string password);
        Task<User> GetUserByIdAsync(Guid Id);
    }
}
