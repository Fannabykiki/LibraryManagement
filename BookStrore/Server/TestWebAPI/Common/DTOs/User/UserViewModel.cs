using Common.Enums;

namespace BookStore.Common.DTOs.User
{
	public class UserViewModel
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public UserRoleEnum Role { get; set; }
	}
}
