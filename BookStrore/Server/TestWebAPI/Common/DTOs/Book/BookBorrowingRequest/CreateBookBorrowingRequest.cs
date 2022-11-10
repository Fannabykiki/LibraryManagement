using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations;


namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
    public class CreateBookBorrowingRequest
    {
        public Guid UserRequestId { get; set; }
        public List<int> BookIds { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
