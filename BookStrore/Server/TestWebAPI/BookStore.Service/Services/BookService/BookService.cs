using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book;
using BookStore.Common.DTOs.Book.BorrowingRequestDetail;
using AutoMapper;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.API.Services.BookService
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IBookRequestRepository _bookRequestRepository;
		private readonly IBorrowingDetailRepository _borrowingDetailRepository;
		private readonly IUserRepository _userRepository;
		private readonly IShippingRepository _shippingRepository;
		private readonly IShippingDetailRepository _shippingDetailRepository;
		private readonly IMapper _mapper;

		public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IBookRequestRepository bookRequestRepository, IBorrowingDetailRepository borrowingDetailRepository, IShippingRepository shippingRepository, IShippingDetailRepository shippingDetailRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_categoryRepository = categoryRepository;
			_bookRequestRepository = bookRequestRepository;
			_borrowingDetailRepository = borrowingDetailRepository;
			_shippingRepository = shippingRepository;
			_shippingDetailRepository = shippingDetailRepository;
			_mapper = mapper;
		}

		public async Task<AddBookResponse> CreateAsync(AddBookRequest addBookRequest)
		{
			using var transaction = _bookRepository.DatabaseTransaction();
			try
			{
				var book = new Books
				{
					BookName = addBookRequest.BookName,
					PublishedDate = addBookRequest.PublishedDate,
					PublisherName = addBookRequest.PublisherName,
					CategoryId = addBookRequest.CategoryId,
				};
				var newBook = await _bookRepository.CreateAsync(book);

				_bookRepository.SaveChanges();
				transaction.Commit();

				return new AddBookResponse
				{
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
				var newBookBorrowingRequest = new BookBorrowingRequest
				{
					UserRquestId = createBookBorrowingRequest.UserRequestId,
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
					var product = await _bookRepository.GetAsync(s => s.BookId == id, x => x.Category);
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

		public async Task<List<BookViewModel>> GetAllBookAsync()
		{
			var books = await _bookRepository.GetAllWithOdata(x => true, x => x.Category);

			return _mapper.Map<List<BookViewModel>>(books);

		}

		public async Task<IEnumerable<BorrowingRequestViewModel>> GetAllBookRequestAsync()
		{
			var borrow = await _bookRequestRepository.GetAllWithOdata(x => true, x => x.User);

			return _mapper.Map<List<BorrowingRequestViewModel>>(borrow);
		}

		public async Task<BookViewModel> GetBookByIdAsync(int id)
		{
			using (var transaction = _categoryRepository.DatabaseTransaction())
				try
				{
					var result = await _bookRepository.GetAsync(c => c.BookId == id, x => x.Category);

					if (result == null)
					{
						return null;
					}

					return new BookViewModel
					{
						BookId = result.BookId,
						BookName = result.BookName,
						PublisherName = result.PublisherName,
						PublishedDate = result.PublishedDate,
						CategoryName = result.Category.CategoryName,
					};
				}
				catch
				{
					transaction.RollBack();

					return null;
				}
		}

		public async Task<IEnumerable<DetailViewModel>> GetBorrowingDetailByRequestIdAsync(int id)
		{
			var result = await _borrowingDetailRepository.GetAllWithOdata(c => c.BookBorrowingRequestId == id, x => x.Book);

			return _mapper.Map<List<DetailViewModel>>(result);
		}

		public async Task<IEnumerable<BorrowingRequestViewModel>> GetRequestByUserId(Guid userId)
		{
			var transaction = _bookRequestRepository.DatabaseTransaction();
			try
			{
				var result = await _bookRequestRepository.GetAllWithOdata(x => x.UserRquestId == userId, x => x.User);
				if (result == null)
				{
					return null;
				}

                return _mapper.Map<List<BorrowingRequestViewModel>>(result);
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
				var book = await _bookRepository.GetAsync(s => s.BookId == id, x => x.Category);
				if (book == null)
				{
					return new UpdateBookResponse
					{
						IsSucced = false,
					};
				}

				book.BookName = updateBookRequest.BookName;
				book.PublishedDate = updateBookRequest.PublishedDate;
				book.PublisherName = updateBookRequest.PublisherName;
				book.CategoryId = updateBookRequest.CategoryId;

				await _bookRepository.UpdateAsync(book);
				_bookRepository.SaveChanges();
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
				};
			}
		}

		public async Task<UpdateBookBorrowingResponse> UpdateBorrowingRequestAsync(UpdateBorrowingRequest updateBorrowingRequest, int id)
		{
			using (var transaction = _bookRepository.DatabaseTransaction())
			{
				try
				{
					var updateRequest = await _bookRequestRepository.GetAsync(s => s.BookBorrowingRequestId == id, x => x.User);
					if (updateRequest == null)
					{
						return new UpdateBookBorrowingResponse
						{
							IsSucced = false,
						};
					}

					updateRequest.Status = updateBorrowingRequest.RequestStatus;
					updateRequest.UserApprovedName = updateBorrowingRequest.UserApprovedName;

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
