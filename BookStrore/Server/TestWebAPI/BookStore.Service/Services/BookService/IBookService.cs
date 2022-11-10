using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book.BookRequest;

namespace BookStore.API.Services.BookService
{
    public interface IBookService
    {
        Task<IEnumerable<Books>> GetAllBookAsync();
        Task<AddBookResponse> CreateAsync(AddBookRequest addBookRequest);
        Task<bool> UpdateAsync(UpdateBookRequest updateBookRequest);
        Task<bool> DeleteAsync(int id);
        Task<BookViewModel> GetBookByIdAsync(int id);
        Task<CreateBookBorrowingResponse> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest);
        Task<IEnumerable<BookBorrowingRequest>> GetAllBookRequestAsync();
        Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(UpdateBorrowingRequest updateBorrowingRequest);
    }
}
