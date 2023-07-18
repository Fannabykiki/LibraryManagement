using BookStore.Common.DTOs.Base;

namespace BookStore.API.DTOs.Category
{
    public class AddCategoryResponse : BaseResponse
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
