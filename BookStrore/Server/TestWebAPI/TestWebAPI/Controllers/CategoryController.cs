using Microsoft.AspNetCore.Mvc;
using BookStore.API.DTOs.Category;
using BookStore.API.Services.CategoryService;
using BookStore.Service.Services.Loggerservice;
using Microsoft.AspNetCore.Authorization;
using Common.Enums;

namespace Book.API.Controllers
{
    [Authorize]
    [Route("/api/category-management")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService, ILoggerManager logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("categories")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoryService.GetAllAsync();
            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperUser)]
        [HttpPost("categories")]
        public async Task<IActionResult> Add([FromBody] AddCategoryRequest addCategoryRequest)
        {
            var result = await _categoryService.CreateAsync(addCategoryRequest);
            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperUser)]
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result)
            {
                return StatusCode(400);
            }
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperUser)]
        [HttpPut("categories/{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            var result = await _categoryService.UpdateAsync(Id, updateCategoryRequest);
            if (result == null) return StatusCode(500);

            return Ok(result);
        }
    }
}

