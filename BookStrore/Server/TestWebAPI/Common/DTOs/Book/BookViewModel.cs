
namespace BookStore.API.DTOs
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CategoryName { get; set; }
    }
}
