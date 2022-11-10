using BookStore.Data.Entities;
using BookStore.API.DTOs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Services.BookService
{
    public interface IBookService
    {
        IEnumerable<Books>? GetAll();
        ActionResult<AddBookResponse>? Add(AddBookRequest addBookRequest);
        ActionResult? Update(UpdateBookRequest updateBookRequest);
        ActionResult? Delete(int id);
        ActionResult<BookViewModel>? GetBookById(int id);
    }
}
