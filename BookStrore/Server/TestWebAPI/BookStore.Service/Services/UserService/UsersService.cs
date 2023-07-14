using BookStore.Common.DTOs.Shipping;
using BookStore.Common.DTOs.ShippingDTO;
using BookStore.Common.DTOs.User;
using BookStore.Data;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Implements;
using BookStore.Data.Repositories.Interfaces;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

		public async Task<IEnumerable<User>> GetAllUserAsync()
		{
			return await _userRepository.GetAllWithOdata(x => true);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			using (var transaction = _userRepository.DatabaseTransaction())
				try
				{
					var product = await _userRepository.GetAsync(s => s.UserId == id);
					if (product == null)
					{
						return false;
					}

					_userRepository.DeleteAsync(product);

					_userRepository.SaveChanges();

					transaction.Commit();

					return true;
				}
				catch (Exception)
				{
					transaction.RollBack();

					return false;
				}
		}

		public async Task<CreateuserResponse> UpdateUserAsync(UpdateUserRequest updateUserRequest, Guid id)
		{
			using (var transaction = _userRepository.DatabaseTransaction())
			{
				try
				{
					var updateRequest = await _userRepository.GetAsync(s => s.UserId == id);
					if (updateRequest == null)
					{
						return new CreateuserResponse
						{
							IsSucced = false,
						};
					}

					updateRequest.PhoneNumber = updateUserRequest.PhoneNumber;
					updateRequest.UserName = updateUserRequest.UserName;
					updateRequest.Address = updateUserRequest.Address;
					if (updateUserRequest.Role != null)
					{
						updateRequest.Role = updateUserRequest.Role;
					}
					else
					{
						updateRequest.Role = updateRequest.Role;
					}

					await _userRepository.UpdateAsync(updateRequest);
					_userRepository.SaveChanges();

					transaction.Commit();

					return new CreateuserResponse
					{
						IsSucced = true,
					};
				}
				catch (Exception)
				{
					transaction.RollBack();

					return new CreateuserResponse
					{
						IsSucced = false,
					};
				}
			}
		}

		public async Task<CreateuserResponse> CreateAsync(CreateUserRequest createUserRequest)
		{
			using var transaction = _userRepository.DatabaseTransaction();
			try
			{
				var newUserRequest = new User
				{
					UserId = Guid.NewGuid(),
					UserName = createUserRequest.UserName,
					Address = createUserRequest.Address,
					PhoneNumber = createUserRequest.PhoneNumber,
					Password = createUserRequest.Password,
					Role = createUserRequest.Role,
				};

			var newShipping = await _userRepository.CreateAsync(newUserRequest);
			_userRepository.SaveChanges();

			transaction.Commit();

			return new CreateuserResponse
			{
				IsSucced = true,
			};
		}
			catch (Exception)
			{
				transaction.RollBack();

				return new CreateuserResponse
				{
					IsSucced = false,
				};
}
		}
	}
}
