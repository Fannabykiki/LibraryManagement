using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BookStoreContext context) : base(context)
        {
        }
    }
}
