using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Category
{
    public class UpdateCategoryRequest
    {
        [Required]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
