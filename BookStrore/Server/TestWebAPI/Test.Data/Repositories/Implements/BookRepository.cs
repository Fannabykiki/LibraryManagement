using BookStore.Common.DTOs.Book;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class BookRepository : BaseRepository<Books>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context)
        {
        }

        public Task<List<Books>> GetAllBooks(GetBooksRequest getBooksRequest)
        {
            return null;
            //var book = (from b in _context.Books
            //              join bc in _context.BookCategoryDetails
            //              on b.BookId equals bc.BookId
            //              join c in _context.Categories
            //              on bc.CategoryId equals c.CategoryId
            //              where c.CategoryId == getBooksRequest.CategoryId
            //              select new
            //              {
            //                 BookId = b.BookId,
            //                 BookName = b.BookName,
                             
            //              }).ToList();
        }
    }
}