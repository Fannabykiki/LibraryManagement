using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book;
using BookStore.Common.DTOs.Book.BorrowingRequestDetail;

namespace BookStore.API.Services.BookService
{
    public interface IBookService
    {
        Task<IEnumerable<Books>> GetAllBookAsync();
        Task<AddBookResponse> CreateAsync(AddBookRequest addBookRequest);
        Task<UpdateBookResponse> UpdateAsync(int id, UpdateBookRequest updateBookRequest);
        Task<bool> DeleteAsync(int id);
        Task<BookViewModel> GetBookByIdAsync(int id);
        Task<CreateBorrowingBookResponse> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest);
        Task<IEnumerable<BookBorrowingRequest>> GetAllBookRequestAsync();
        Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(UpdateBorrowingRequest updateBorrowingRequest,int id);
        //Task<GetBooksResponse> GetBooks(GetBooksRequest getBookRequest);
        Task<BorrowingDetailResponse> GetBorrowingDetailByRequestIdAsync(int id);
        Task<IEnumerable<GetBookResponse>> GetRequestByUserId(User user);
    }
}
