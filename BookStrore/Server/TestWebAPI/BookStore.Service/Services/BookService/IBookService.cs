using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book;

namespace BookStore.API.Services.BookService
{
    public interface IBookService
    {
        Task<IEnumerable<Books>> GetAllBookAsync();
        Task<AddBookResponse> CreateAsync(AddBookRequest addBookRequest);
        Task<UpdateBookResponse> UpdateAsync(int id,UpdateBookRequest updateBookRequest);
        Task<bool> DeleteAsync(int id);
        Task<BookViewModel> GetBookByIdAsync(int id);
        Task<CreateBorrowingBookResponse> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest);
        Task<IEnumerable<BookBorrowingRequest>> GetAllBookRequestAsync();
        Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(Guid userApproveId,UpdateBorrowingRequest updateBorrowingRequest);
        Task<GetBooksResponse> GetBooks(GetBooksRequest getBookRequest);
    }
}
