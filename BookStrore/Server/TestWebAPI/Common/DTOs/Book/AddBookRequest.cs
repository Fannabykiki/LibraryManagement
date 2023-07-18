using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class AddBookRequest
    {
        [Required]
        public string BookName { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; }
    }
}
