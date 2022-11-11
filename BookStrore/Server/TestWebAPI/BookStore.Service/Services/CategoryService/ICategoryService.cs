using BookStore.Data.Entities;
using BookStore.API.DTOs.Category;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Category;

namespace BookStore.API.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<AddCategoryResponse> CreateAsync(AddCategoryRequest addCategoryRequest);
        Task<UpdateCategoryResponse> UpdateAsync(int Id, UpdateCategoryRequest updateCategoryRequest);
        Task<bool> DeleteAsync(int id);
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        //Task<BookCategoryDetail> GetBookByCategoryIdAsync(int id);
    }
}
