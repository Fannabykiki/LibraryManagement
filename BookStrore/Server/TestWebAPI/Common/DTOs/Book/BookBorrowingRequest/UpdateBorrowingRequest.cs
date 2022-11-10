using BookStore.Common.Enums;

namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
    public class UpdateBorrowingRequest
    {
        public int BorrowingId { get; set; }
        public RequestStatusEnum RequestStatus { get; set; }
    }
}
