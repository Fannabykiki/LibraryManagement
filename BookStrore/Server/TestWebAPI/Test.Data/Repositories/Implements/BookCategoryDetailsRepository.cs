using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class BookCategoryDetailsRepository : BaseRepository<BookCategoryDetail>, IBookCategoryDetail
    {
        public BookCategoryDetailsRepository(BookStoreContext context) : base(context)
        {
        }
    }
}