using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Category
{
    public class AddCategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}
