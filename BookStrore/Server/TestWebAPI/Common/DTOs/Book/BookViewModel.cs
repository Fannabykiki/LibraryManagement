
namespace BookStore.API.DTOs
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public List<int>? CategoryIds { get; set; }
    }
}
