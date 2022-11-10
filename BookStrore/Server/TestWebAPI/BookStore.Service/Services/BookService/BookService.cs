using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book.BookRequest;
using BookStore.Data.Repositories.Implements;

namespace BookStore.API.Services.BookService
{

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookCategoryDetail _detailRepository;
        private readonly IBookRequestRepository _bookRequestRepository;
        private readonly IBorrowingDetailRepository _borrowingDetailRepository;
        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IBookCategoryDetail bookCategoryDetail, IBookRequestRepository bookRequestRepository, IBorrowingDetailRepository borrowingDetailRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _detailRepository = bookCategoryDetail;
            _bookRequestRepository = bookRequestRepository;
            _borrowingDetailRepository = borrowingDetailRepository;
        }

        public async Task<AddBookResponse> CreateAsync(AddBookRequest addBookRequest)
        {
            using var transaction = _bookRepository.DatabaseTransaction();
            try
            {
                var book = new Books
                {
                    BookName = addBookRequest.BookName
                };
                var newBook = await _bookRepository.CreateAsync(book);

                foreach (var categoryId in addBookRequest.CategoryIds)
                {
                    var category = await _categoryRepository.GetAsync(s => s.CategoryId == categoryId);
                    if (category == null)
                    {
                        return null;
                    }
                    if (category != null)
                    {
                        var newBookCategoryDetail = new BookCategoryDetail
                        {
                            Book = newBook,
                            Category = category
                        };
                        _detailRepository.CreateAsync(newBookCategoryDetail);
                    }
                }
                _bookRepository.SaveChanges();
                _detailRepository.SaveChanges();

                transaction.Commit();

                return new AddBookResponse
                {
                    BookId = newBook.BookId,
                    BookName = newBook.BookName,
                    CategoryIds = addBookRequest.CategoryIds
                };
            }
            catch (Exception)
            {
                transaction.RollBack();

                return null;
            }
        }

        public async Task<CreateBookBorrowingResponse> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest)
        {
            using var transaction = _bookRequestRepository.DatabaseTransaction();
            try
            {
                var newBookBorrowingRquest = new BookBorrowingRequest
                {

                    UserRquestId  = createBookBorrowingRequest.UserRequestId,
                    RequestDate = createBookBorrowingRequest.RequestDate,
                    Status = Common.Enums.RequestStatusEnum.Pending
                };

                var newBookRequest = await _bookRequestRepository.CreateAsync(newBookBorrowingRquest);
                _bookRequestRepository.SaveChanges();
                foreach (var bookId in createBookBorrowingRequest.BookIds)
                {
                    var requestDetail = new BookBorrowingRequestDetails
                    {
                       BookId = bookId,
                       BookBorrowingRequestId = newBookRequest.BookBorrowingRequestId,
                    };
                   await _borrowingDetailRepository.CreateAsync(requestDetail);
                    _borrowingDetailRepository.SaveChanges();
                }

                transaction.Commit();
                return new CreateBookBorrowingResponse
                {
                    IsSucced = true,
                };
            }
            catch (Exception)
            {
                transaction.RollBack();

                return new CreateBookBorrowingResponse
                {
                    IsSucced = false,
                }; ;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
                try
                {
                    var product = await _bookRepository.GetAsync(s => s.BookId == id);
                    if (product == null)
                    {
                        return false;
                    }

                    _bookRepository.DeleteAsync(product);
                    _bookRepository.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return false;
                }
        }

        public async Task<bool> UpdateBorrowingRequestAsync(int id)
        {
            
            return false;
        }

        public async Task<IEnumerable<Books>> GetAllBookAsync()
        {
            return await _bookRepository.GetAllAsync(x => true);

        }

        public async Task<IEnumerable<BookBorrowingRequest>> GetAllBookRequestAsync()
        {
            return await _bookRequestRepository.GetAllAsync(x => true);

        }

        public async Task<BookViewModel> GetBookByIdAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var result = await _bookRepository.GetAsync(c => c.BookId == id);

                    if (result == null)
                    {
                        return null;
                    }

                    return new BookViewModel
                    {
                        BookId = result.BookId,
                        BookName = result.BookName,
                        CategoryIds = _detailRepository.GetAll(x => x.BookId == id).Select(x => x.CategoryId).ToList()
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public async Task<bool> UpdateAsync(UpdateBookRequest updateBookRequest)
        {
            using var transaction = _bookRepository.DatabaseTransaction();
            try
            {
                foreach (var categoryId in updateBookRequest.CategoryIds)
                {
                    var category = await _categoryRepository.GetAllAsync(s => s.CategoryId == categoryId);

                    var book = await _bookRepository.GetAsync(s => s.BookId == updateBookRequest.BookId);
                    if (book != null)
                    {
                        book.BookName = updateBookRequest.BookName;

                        var newBook = await _bookRepository.UpdateAsync(book);

                        var newBookCategoryDetail = new BookCategoryDetail
                        {
                            Book = newBook,
                            Category = (Category)category
                        };
                        await _detailRepository.UpdateAsync(newBookCategoryDetail);
                    }
                }
                _bookRepository.SaveChanges();
                _detailRepository.SaveChanges();

                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                transaction.RollBack();

                return false;
            }
        }

        public async Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(UpdateBorrowingRequest updateBorrowingRequest)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
            {
                try
                {
                    var updateRequest = await _bookRequestRepository.GetAsync(s => s.BookBorrowingRequestId == updateBorrowingRequest.BorrowingId);
                    if (updateRequest == null)
                    {
                        return new UpdateBookBorrowingResponse
                        {
                            IsSucced = false,
                        };
                    }

                    updateRequest.Status = updateBorrowingRequest.RequestStatus;

                     await _bookRequestRepository.UpdateAsync(updateRequest);

                    _bookRequestRepository.SaveChanges();

                    transaction.Commit();

                    return new UpdateBookBorrowingResponse
                    {
                        IsSucced = true,
                    }; 
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return new UpdateBookBorrowingResponse
                    {
                        IsSucced = false,
                    };
                }
            }
        }
    }
}
