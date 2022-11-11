using Microsoft.AspNetCore.Mvc;
using BookStore.API.DTOs.Category;
using BookStore.Data.Entities;
using BookStore.API.Services.CategoryService;

namespace Book.API.Controllers
{
    [Route("/api/category-management")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryById(int id)
        {
            return  await _categoryService.GetCategoryByIdAsync(id);
        }

        [HttpPost("categories")]
        public async Task<ActionResult<AddCategoryResponse>> Add([FromBody] AddCategoryRequest addCategoryRequest)
        {
            return await _categoryService.CreateAsync(addCategoryRequest);
        }

        [HttpDelete("categories/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _categoryService.DeleteAsync(id);
        }
        [HttpPut("categories")]
        public async Task<bool> Update([FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            return await  _categoryService.UpdateAsync(updateCategoryRequest);
        }

        //[HttpGet("categories-details/{id}")]
        //public async Task<BookCategoryDetail> GetBooksByCategoryId(int id)
        //{
        //    return await _categoryService.GetBookByCategoryIdAsync(id);
        //}
    }
}
