using BookStore.Common.Enums;

namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
    public class UpdateBorrowingRequest
    {
        public RequestStatusEnum RequestStatus { get; set; }
    }
}
