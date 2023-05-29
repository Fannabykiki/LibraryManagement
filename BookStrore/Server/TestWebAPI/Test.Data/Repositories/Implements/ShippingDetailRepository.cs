using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class ShippingDetailRepository : BaseRepository<ShippingDetail>, IShippingDetailRepository
    {
        public ShippingDetailRepository(BookStoreContext context) : base(context)
        {
        }
    }
}
