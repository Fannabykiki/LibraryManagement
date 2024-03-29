﻿using BookStore.API.DTOs;
using BookStore.API.Extensions.Validation;
using BookStore.API.Services.BookService;
using BookStore.API.Services.UserService;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Data.Entities;
using BookStore.Extensions;
using BookStore.Service.Services.Loggerservice;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Book.API.Controllers
{
	[Authorize]
	[Route("/api/book-management")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly ILoggerManager _logger;
		private readonly IBookService _bookService;
		private readonly IUsersService _usersService;

		public BookController(IBookService bookService, ILoggerManager logger, IUsersService usersService)
		{
			_bookService = bookService;
			_logger = logger;
			_usersService = usersService;
		}

		[EnableQuery]
        [HttpGet("books")]
		[Authorize(Roles = UserRoles.Admin)]
		public async Task<ActionResult<IQueryable<BookViewModel>>> GetAllBook()
		{
			var result = await _bookService.GetAllBookAsync();
			if (result == null) return StatusCode(500);
			return Ok(result);
		}

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("books")]
		public async Task<IActionResult> Create([FromBody] AddBookRequest addBook)
		{
			if (!ModelState.IsValid)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ModelState);
			}

			var result = await _bookService.CreateAsync(addBook);

			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[HttpDelete("books/{id}")]
		[Authorize(Roles = UserRoles.Admin)]
		
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _bookService.DeleteAsync(id);

			return Ok(result);
		}

		[HttpPut("books/{id}")]
		[Authorize(Roles = UserRoles.Admin)]

		public async Task<IActionResult> Update(int id, [FromBody] UpdateBookRequest updateBookRequest)
		{	

			var result = await _bookService.UpdateAsync(id, updateBookRequest);

			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpGet("books/{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var result = await _bookService.GetBookByIdAsync(id);

			if (result == null) return NotFound();

			return Ok(result);
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPost("book-borrowing")]
		public async Task<IActionResult> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest)
		{
			var result = await _bookService.CreateBookBorrowing(createBookBorrowingRequest);

			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[EnableQuery]
		[Authorize(Roles = UserRoles.Admin)]
		[HttpGet("book-borrowing")]
		public async Task<IEnumerable<BorrowingRequestViewModel>> GetAllBookRequest()
		{
			return await _bookService.GetAllBookRequestAsync();
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPut("book-borrowing/{id}")]
		public async Task<IActionResult> UpdateBorrowingRequest(int id, UpdateBorrowingRequest updateBorrowingRequest)
		{

			var result = await _bookService.UpdateBorrowingRequestAsync(updateBorrowingRequest, id);

			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[Authorize]
		[HttpGet("book-borrowingdetail/{id}")]
		public async Task<IActionResult> GetBorrowingDetailByRequestIdAsync(int id)
		{
			var result = await _bookService.GetBorrowingDetailByRequestIdAsync(id);

			if (result == null) return NotFound();

			return Ok(result);
		}

		[HttpGet("book-borrowingrequest")]
		public async Task<IActionResult> GetRequestByUserId()
		{
			var userId = this.GetCurrentLoginUserId();
			if (userId == null)
			{
				return NotFound();
			}
			if (userId != null)
			{
				var result = await _bookService.GetRequestByUserId(userId.UserId);
				if (result != null)
				{
					return Ok(result);
				}
				else
					return BadRequest();
			}
			else
				return BadRequest();
			
		}
	}
}
