using BookStore.Common.Enums;

namespace BookStore.Common.DTOs.Book
{
    public class GetBooksRequest
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
    }
}