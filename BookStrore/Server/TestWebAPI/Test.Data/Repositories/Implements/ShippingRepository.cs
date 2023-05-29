using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class ShippingRepository : BaseRepository<Shipping>, IShippingRepository
    {
        public ShippingRepository(BookStoreContext context) : base(context)
        {
        }
    }
}
