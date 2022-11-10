using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class UpdateBookRequest
    {
        [Required]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
