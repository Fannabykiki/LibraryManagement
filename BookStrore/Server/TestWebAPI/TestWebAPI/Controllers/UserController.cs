using BookStore.API.Services.UserService;
using BookStore.Common.DTOs.Shipping;
using BookStore.Common.DTOs.ShippingDTOs;
using BookStore.Common.DTOs.User;
using BookStore.Data.Entities;
using BookStore.Service.Services.Loggerservice;
using BookStore.Service.Services.ShippingService;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BookStore.API.Controllers
{
	[Authorize]
    [Route("api/user-management")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ILoggerManager _logger;
		private readonly IUsersService _usersService;

		public UserController(ILoggerManager logger, IUsersService usersService)
		{
			_logger = logger;
			_usersService = usersService;
		}

		[Authorize(Roles = UserRoles.Admin)]
		[EnableQuery]
		[HttpGet("users")]
		public async Task<ActionResult<IQueryable<User>>> GetAllShipping()
		{
			var result = await _usersService.GetAllUserAsync();
			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPost("users")]
		public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest)
		{
			var result = await _usersService.CreateAsync(createUserRequest);
			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpDelete("users/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var rs = await _usersService.GetUserByIdAsync(id);
			if (rs == null) return NotFound();
			var result = await _usersService.DeleteAsync(id);

			return Ok(result);
		}

		[Authorize]
		[HttpPut("users/{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest updateUserRequest)
		{
			var rs = await _usersService.GetUserByIdAsync(id);
			if (rs == null) return NotFound();
			var result = await _usersService.UpdateUserAsync(updateUserRequest, id);
			if (result == null) return StatusCode(500);

			return Ok(result);
		}


	}
}
