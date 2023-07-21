using BookStore.Common.DTOs.User;
using BookStore.Data.Entities;

namespace BookStore.API.Services.UserService
{
    public interface IUsersService
    {
        Task<User> LoginUser(string username, string password);
        Task<User> GetUserByIdAsync(Guid Id);
		Task<IEnumerable<User>> GetAllUserAsync();
		Task<bool> DeleteAsync(Guid id);
		Task<CreateuserResponse> UpdateUserAsync(UpdateUserRequest updateUserRequest, Guid id);
		Task<CreateuserResponse> CreateAsync(CreateUserRequest createUserRequest);
	}
}
