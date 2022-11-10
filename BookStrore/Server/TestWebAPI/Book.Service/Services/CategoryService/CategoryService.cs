using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs.Category;
using BookStore.API.Services.CategoryService;

namespace BookStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public AddCategoryResponse? Add(AddCategoryRequest addCategoryRequest)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var addCategory = new Category
                    {
                        CategoryName = addCategoryRequest.CategoryName
                    };
                    var category = _categoryRepository.Create(addCategory);

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

        public CategoryViewModel? GetCategoryById(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = _categoryRepository.Get(c => c.CategoryId == id);

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

        public CategoryViewModel? Update(UpdateCategoryRequest updateCategoryRequest)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = _categoryRepository.Get(s => s.CategoryId == updateCategoryRequest.CategoryId);
                    if (category != null)
                    {
                        category.CategoryId = updateCategoryRequest.CategoryId;
                        category.CategoryName = updateCategoryRequest.CategoryName;
                        _categoryRepository.SaveChanges();
                    }
                    _categoryRepository.Update(category);

                    transaction.Commit();

                    return null;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return null;
                }
        }

        CategoryViewModel? ICategoryService.Delete(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = _categoryRepository.Get(s => s.CategoryId == id);
                    if (category != null)
                    {
                        _categoryRepository.Delete(category);
                        _categoryRepository.SaveChanges();
                    }
                    transaction.Commit();

                    return null;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return null;
                }
        }

        IEnumerable<Category>? ICategoryService.GetAll()
        {
            return _categoryRepository.GetAll(x => true);
        }
    }
}
