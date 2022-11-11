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
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _categoryService.GetAllAsync();
                if (result == null) return StatusCode(500);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryByIdAsync(id);
                if (result == null) return StatusCode(500);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("categories")]
        public async Task<IActionResult> Add([FromBody] AddCategoryRequest addCategoryRequest)
        {
            try
            {
                var result = await _categoryService.CreateAsync(addCategoryRequest);
                if (result == null) return StatusCode(500);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryService.DeleteAsync(id);

                if (!result)
                {
                    return StatusCode(400);
                }

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }

        }
        [HttpPut("categories")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            try
            {
                var result = await _categoryService.UpdateAsync(updateCategoryRequest);
                if (result == null) return StatusCode(500);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }

    //[HttpGet("categories-details/{id}")]
    //public async Task<BookCategoryDetail> GetBooksByCategoryId(int id)
    //{
    //    return await _categoryService.GetBookByCategoryIdAsync(id);
    //}
}

