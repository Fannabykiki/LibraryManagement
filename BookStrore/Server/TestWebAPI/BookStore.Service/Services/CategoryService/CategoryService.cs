using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs.Category;
using BookStore.API.Services.CategoryService;
using BookStore.API.DTOs;
using BookStore.Data.Repositories;

namespace BookStore.Services.CategoryService
{
    public class CategoryService : IBookRequestService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookCategoryDetail _detailRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<AddCategoryResponse> CreateAsync(AddCategoryRequest addCategoryRequest)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var addCategory = new Category
                    {
                        CategoryName = addCategoryRequest.CategoryName
                    };
                    var category = await _categoryRepository.CreateAsync(addCategory);

                    _categoryRepository.SaveChanges();

                    transaction.Commit();

                    return new AddCategoryResponse
                    {
                        CategoryName = category.CategoryName,
                        CategoryId = category.CategoryId
                    };
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public async Task<IEnumerable<BookCategoryDetail>> GetBookByCategoryIdAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(c => c.CategoryId == id);

                    if (category == null)
                    {
                        return null;
                    }

                    return null;

                    _detailRepository.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(c => c.CategoryId == id);

                    if (category == null)
                    {
                        return null;
                    }

                    _categoryRepository.SaveChanges();

                    transaction.Commit();

                    return new CategoryViewModel
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public async Task<bool> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(s => s.CategoryId == updateCategoryRequest.CategoryId);
                    if (category != null)
                    {
                        category.CategoryId = updateCategoryRequest.CategoryId;
                        category.CategoryName = updateCategoryRequest.CategoryName;
                        _categoryRepository.SaveChanges();
                    }
                    _categoryRepository.UpdateAsync(category);

                    transaction.Commit();

                    return false;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return false;
                }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(s => s.CategoryId == id);
                    if (category != null)
                    {
                        _categoryRepository.DeleteAsync(category);
                        _categoryRepository.SaveChanges();
                    }
                    transaction.Commit();

                    return false;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return false;
                }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync(x => true);
        }
    }
}
