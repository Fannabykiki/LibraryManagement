using BookStore.Common.DTOs.Book;
using BookStore.Data.Entities;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Books>
    {
        Task<List<Books>> GetAllBooks(GetBooksRequest getBooksRequest);
    }
}
