using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class UpdateBookRequest
    {
        public string BookName { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
