using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book;

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
                        return new AddBookResponse
                        {
                            IsSucced = false,
                        };
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
                    CategoryIds = addBookRequest.CategoryIds,
                    IsSucced = true
                };
            }
            catch (Exception)
            {
                transaction.RollBack();

                return new AddBookResponse
                {
                    IsSucced = false,
                };
            }
        }

        public async Task<CreateBorrowingBookResponse> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest)
        {
            using var transaction = _bookRequestRepository.DatabaseTransaction();
            try
            {
                var countUserRequest = (await _bookRequestRepository.GetAllAsync(b => b.UserRquestId == createBookBorrowingRequest.UserRequestId));

                

                var newBookBorrowingRequest = new BookBorrowingRequest
                {
                    UserRquestId = createBookBorrowingRequest.UserRequestId,
                    RequestDate = createBookBorrowingRequest.RequestDate,
                    Status = Common.Enums.RequestStatusEnum.Pending
                };

                var newBookRequest = await _bookRequestRepository.CreateAsync(newBookBorrowingRequest);

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
                return new CreateBorrowingBookResponse
                {
                    IsSucced = true,
                };
            }
            catch (Exception)
            {
                transaction.RollBack();

                return new CreateBorrowingBookResponse
                {
                    IsSucced = false,
                };
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

        public async Task<GetBooksResponse> GetBooks(GetBooksRequest getBookRequest)
        {
            //using (var transaction = _bookRepository.DatabaseTransaction())
            //    try
            //    {
            //        var books = await _bookRepository.GetAllAsync(x => (getBookRequest.BookId == 0 || x.BookId == getBookRequest.BookId));
            //        if (books == null)
            //        {
            //            return new GetBooksResponse
            //            {
            //                IsSucced = false
            //            };
            //        }

            //        var getBookResponse = new GetBooksResponse
            //        {
            //            BookViewModels = books.,
            //        };
            //        foreach (var book in books)
            //        {
            //            var bookCategoryDetailIds = (await _detailRepository.GetAllAsync(x => x.BookId == book.BookId))
            //                                                                .Select(x => x.CategoryId);
            //            var categories = await _categoryRepository.GetAllAsync(x => bookCategoryDetailIds.Contains(x.CategoryId));

            //        }

            //    }
            //    catch (Exception ex)
            //    {

            //    }
            return null;
        }

        public async Task<UpdateBookResponse> UpdateAsync(UpdateBookRequest updateBookRequest)
        {
            using var transaction = _bookRepository.DatabaseTransaction();
            try
            {
                var book = await _bookRepository.GetAsync(s => s.BookId == updateBookRequest.BookId);
                if (book == null)
                {
                    return new UpdateBookResponse
                    {
                        IsSucced = false,
                    };
                }

                book.BookName = updateBookRequest.BookName;

                var newBook = await _bookRepository.UpdateAsync(book);

                _bookRepository.SaveChanges();

                var bookCategories = await _detailRepository.GetAllAsync(s => s.BookId == updateBookRequest.BookId);
                foreach (var item in bookCategories)
                {
                    await _detailRepository.DeleteAsync(item);
                    _detailRepository.SaveChanges();

                }
                foreach (var categoryId in updateBookRequest.CategoryIds)
                {
                    var category = await _categoryRepository.GetAsync(s => s.CategoryId == categoryId);

                    var newBookCategoryDetail = new BookCategoryDetail
                    {
                        Book = newBook,
                        Category = category
                    };
                    await _detailRepository.CreateAsync(newBookCategoryDetail);
                }
                _detailRepository.SaveChanges();

                transaction.Commit();

                return new UpdateBookResponse
                {
                    IsSucced = true,
                };
            }
            catch (Exception)
            {
                transaction.RollBack();

                return new UpdateBookResponse
                {
                    IsSucced = false,
                }; ;
            }
        }

        public async Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(Guid userApproveId ,UpdateBorrowingRequest updateBorrowingRequest)
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
                    updateRequest.UserApproveId = userApproveId;

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
