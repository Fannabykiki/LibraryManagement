using BookStore.Common.DTOs.Base;
using BookStore.Common.Enums;

namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
    public class GetBookResponse : BaseResponse
    {
        public Guid UserRquestId { get; set; }
        public Guid? UserApproveId { get; set; }
        public RequestStatusEnum Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
