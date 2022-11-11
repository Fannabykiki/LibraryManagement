using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IBookRequestRepository : IBaseRepository<BookBorrowingRequest>
    {
    }
}
