using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Entities
{
    public class BookBorrowingRequestDetails
    {
        public int BookId { get; set; }
        public Books Book { get; set; }
        public int BookBorrowingRequestId { get; set; }
        public BookBorrowingRequest BookBorrowingRequest { get; set; }
    }
}
