using BookStore.Common.DTOs.Base;
namespace BookStore.Common.DTOs.Book.BorrowingRequestDetail
{
    public class BorrowingDetailResponse: BaseResponse
    {
       public int BookId { get; set; }
       public int RequestId { get; set; }
    }
}