using BookStore.API.DTOs;
using BookStore.API.Services.BookService;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers
{
    //[Authorize]
    [Route("/api/book-management")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }
        //[Authorize(Roles = UserRoles.SuperUser)]
        //[Authorize(Roles = UserRoles.NormalUser)]
        [AllowAnonymous]
        [HttpGet("books")]
        public async Task<IEnumerable<Books>> GetAllBook()
        {
            return await _bookService.GetAllBookAsync();
        }

        [HttpPost("books")]
        public async Task<ActionResult<AddBookResponse>> Create([FromBody] AddBookRequest addBook)
        {
            try
            {
                var result =  await _bookService.CreateAsync(addBook);

                if (result == null) return StatusCode(500);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("books/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result =await _bookService.DeleteAsync(id);

                if (!result)
                {
                    return StatusCode(400);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("books")]
        public async Task<IActionResult> Update([FromBody] UpdateBookRequest updateBookRequest)
        {
            try
            {
                var result = await _bookService.UpdateAsync(updateBookRequest);

                if (result == null) return StatusCode(500);

                return Ok(result);
            } catch (Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet("books/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var result = await _bookService.GetBookByIdAsync(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }          
        }

        [HttpPost("book-borrowing")]
        public async Task<IActionResult> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest)
        {   
            
            var result = await _bookService.CreateBookBorrowing(createBookBorrowingRequest);

            if (result.IsSucced)
            {
                return Ok(result);
            }

            return StatusCode(500);
             
        }
        [HttpGet("book-borrowing")]
        public async Task<IEnumerable<BookBorrowingRequest>> GetAllBookRequest()
        {
            return await _bookService.GetAllBookRequestAsync();
        }

        [HttpPut("book-borrowing/{id}")]
        public async Task<IActionResult> UpdateBorrowingRequest(UpdateBorrowingRequest updateBorrowingRequest)
        {
            var result = await _bookService.UpdateBorrowingRequestAsync(updateBorrowingRequest);

            if (result.IsSucced)
            {
                return Ok(result);
            }

            return StatusCode(500);
        }
    }
}
