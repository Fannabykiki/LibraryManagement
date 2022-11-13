using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book;
using BookStore.Common.DTOs.Book.BorrowingRequestDetail;

namespace BookStore.API.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookCategoryDetail _detailRepository;
        private readonly IBookRequestRepository _bookRequestRepository;
        private readonly IBorrowingDetailRepository _borrowingDetailRepository;
        private readonly IUserRepository _userRepository;

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

        public async Task<CreateBorrowingBookResponse> CreateBookBorrowing(CreateBookBorrowingRequest createBookBorrowingRequest, User user)
        {
            using var transaction = _bookRequestRepository.DatabaseTransaction();
            try
            {
                var newBookBorrowingRequest = new BookBorrowingRequest
                {
                    UserRquestId = user.UserId,
                    RequestDate = DateTime.UtcNow,
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

        public async Task<BorrowingDetailResponse> GetBorrowingDetailByRequestIdAsync(int id)
        {
            using (var transaction = _borrowingDetailRepository.DatabaseTransaction())
                try
                {
                    var result = await _borrowingDetailRepository.GetAllAsync(c => c.BookBorrowingRequestId == id);
                    if (result == null)
                    {
                        return new BorrowingDetailResponse
                        {
                            IsSucced = false
                        };
                    }
                    foreach (var item in result)
                    {
                        return new BorrowingDetailResponse
                        {
                            BookId = item.BookId,
                            RequestId = id
                        };
                    }
                    return new BorrowingDetailResponse
                    {
                        IsSucced = true
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return new BorrowingDetailResponse
                    {
                        IsSucced = false
                    };
                }
        }

        public async Task<IEnumerable<GetBookResponse>> GetRequestByUserId(User user)
        {
            var transaction = _bookRequestRepository.DatabaseTransaction();
            try
            {
                var result = (await _bookRequestRepository.GetAllAsync(x => x.UserRquestId == user.UserId)).ToList();
                if (result == null)
                {
                    return null;
                }

                return result.Select(bookRequest => new GetBookResponse
                {
                    RequestDate = bookRequest.RequestDate,
                    Status = bookRequest.Status,
                    UserRquestId = user.UserId,
                });
            }
            catch
            {
                transaction.RollBack();

                return null;
            }

        }
        public async Task<UpdateBookResponse> UpdateAsync(int id, UpdateBookRequest updateBookRequest)
        {
            using var transaction = _bookRepository.DatabaseTransaction();
            try
            {
                var book = await _bookRepository.GetAsync(s => s.BookId == id);
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

                var bookCategories = await _detailRepository.GetAllAsync(s => s.BookId == id);
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

        public async Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(User user, UpdateBorrowingRequest updateBorrowingRequest, int id)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
            {
                try
                {
                    var updateRequest = await _bookRequestRepository.GetAsync(s => s.BookBorrowingRequestId == id);
                    if (updateRequest == null)
                    {
                        return new UpdateBookBorrowingResponse
                        {
                            IsSucced = false,
                        };
                    }

                    updateRequest.Status = updateBorrowingRequest.RequestStatus;
                    updateRequest.UserApproveId = user.UserId;

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
