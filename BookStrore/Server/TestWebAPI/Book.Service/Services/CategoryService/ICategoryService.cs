using BookStore.Data.Entities;
using BookStore.API.DTOs.Category;

namespace BookStore.API.Services.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<Category>? GetAll();
        AddCategoryResponse? Add(AddCategoryRequest addCategoryRequest);
        CategoryViewModel? Update(UpdateCategoryRequest updateCategoryRequest);
        CategoryViewModel? Delete(int id);
        CategoryViewModel? GetCategoryById(int id);
    }
}
