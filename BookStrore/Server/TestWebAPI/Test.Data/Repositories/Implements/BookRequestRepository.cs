using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class BookRequestRepository : BaseRepository<BookBorrowingRequest>, IBookRequestRepository
    {
        public BookRequestRepository(BookStoreContext context) : base(context)
        {
        }
    }
}
