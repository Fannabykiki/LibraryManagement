using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Category
{
    public class UpdateCategoryRequest
    {
        public string? CategoryName { get; set; }
    }
}
