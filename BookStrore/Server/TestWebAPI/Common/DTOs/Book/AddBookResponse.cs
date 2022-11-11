using BookStore.Common.DTOs.Base;

namespace BookStore.API.DTOs
{
    public class AddBookResponse : BaseResponse
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
