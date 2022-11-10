using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;


namespace BookStore.Data.Repositories.Implements
{
    public class BookRepository : BaseRepository<Books>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context)
        {
        }
    }
}