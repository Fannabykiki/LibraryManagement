using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookCategoryDetail _detailRepository;
        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IBookCategoryDetail bookCategoryDetail)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _detailRepository = bookCategoryDetail;
        }
        public ActionResult<AddBookResponse>? Add(AddBookRequest addBookRequest)
        {
            using var transaction = _bookRepository.DatabaseTransaction();
            try
            {
                var book = new Books
                {
                    BookName = addBookRequest.BookName
                };
                var newBook = _bookRepository.Create(book);

                foreach (var categoryId in addBookRequest.CategoryIds)
                {
                    var category = _categoryRepository.Get(s => s.CategoryId == categoryId);
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
                        _detailRepository.Create(newBookCategoryDetail);
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

        public ActionResult? Delete(int id)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
                try
                {
                    var product = _bookRepository.Get(s => s.BookId == id);
                    if (product != null)
                    {
                        _bookRepository.Delete(product);
                        _bookRepository.SaveChanges();
                    }
                    transaction.Commit();

                    return null;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public IEnumerable<Books> GetAll()
        {              
             return _bookRepository.GetAll(x => true);
        }

        public ActionResult<BookViewModel>? GetBookById(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var book = _bookRepository.Get(c => c.BookId == id);

                    if (book == null)
                    {
                        return null;
                    }

                    _bookRepository.SaveChanges();

                    transaction.Commit();

                    return new BookViewModel
                    {
                        BookId = book.BookId,
                        BookName = book.BookName,
                        CategoryIds = _detailRepository.GetAll(x => x.Book.BookId == id)                                                  
                                                    .Select(x => x.Category.CategoryId)
                                                    .ToList()
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public ActionResult? Update(UpdateBookRequest updateBookRequest)
        {
            using var transaction = _bookRepository.DatabaseTransaction();
            try
            {
                foreach (var categoryId in updateBookRequest.CategoryIds)
                {
                    var category = _categoryRepository.Get(s => s.CategoryId == categoryId);
                    var book = _bookRepository.Get(s => s.BookId == updateBookRequest.BookId);
                    if (book != null)
                    {
                        book.BookName = updateBookRequest.BookName;

                        var newBook = _bookRepository.Update(book);

                        var newBookCategoryDetail = new BookCategoryDetail
                        {
                            Book = newBook,
                            Category = category
                        };
                        _detailRepository.Update(newBookCategoryDetail);
                    }
                }
                _bookRepository.SaveChanges();
                _detailRepository.SaveChanges();
                transaction.Commit();

                return null;
            }
            catch (Exception)
            {
                transaction.RollBack();

                return null;
            }
        }
    }
}
