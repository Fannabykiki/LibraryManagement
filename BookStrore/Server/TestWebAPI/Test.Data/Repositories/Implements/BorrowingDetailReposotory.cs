using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class BorrowingDetailReposotory : BaseRepository<BookBorrowingRequestDetails> , IBorrowingDetailRepository
    {
        public BorrowingDetailReposotory(BookStoreContext context) : base(context)
        {
        }
    }
}
