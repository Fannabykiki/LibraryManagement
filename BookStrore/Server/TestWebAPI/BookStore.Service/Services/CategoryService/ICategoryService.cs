using BookStore.Data.Entities;
using BookStore.API.DTOs.Category;
using BookStore.API.DTOs;

namespace BookStore.API.Services.CategoryService
{
    public interface IBookRequestService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<AddCategoryResponse> CreateAsync(AddCategoryRequest addCategoryRequest);
        Task<bool> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
        Task<bool> DeleteAsync(int id);
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task<IEnumerable<BookCategoryDetail>> GetBookByCategoryIdAsync(int id);
    }
}
